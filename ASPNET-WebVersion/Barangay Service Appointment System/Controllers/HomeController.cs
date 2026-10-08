using Barangay_Service_Appointment_System.Data;
using Barangay_Service_Appointment_System.Models;
using Barangay_Service_Appointment_System.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Barangay_Service_Appointment_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            var resident = _context.Residences.FirstOrDefault(r => r.Email == userEmail);

            if (resident == null)
            {
                return NotFound("Resident profile not found.");
            }

            var appointments = _context.Appointments
                .Where(a => a.ResidentID == resident.ResidentID)
                .OrderByDescending(a => a.DateSubmitted)
                .ToList();

            var complaints = _context.Complaints
                .Where(c => c.ResidentID == resident.ResidentID)
                .OrderByDescending(c => c.DateSubmitted)
                .ToList();

            var documentServicesList = _context.DocumentServices
                .Where(ds => ds.IsActive == true)
                .ToList();

            var serviceDetailsList = _context.ServiceDetails.ToList();

            var savedRelatives = _context.Relatives
                .Where(r => r.UserID == resident.ResidentID)
                .ToList();

            var viewModel = new ResidentDashboardViewModel
            {
                Resident = resident,
                Appointments = appointments,
                Complaints = complaints,
                DocumentServicesList = documentServicesList,
                ServiceDetailsList = serviceDetailsList,
                SavedRelatives = savedRelatives
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(
    int ResidentID,
    string FirstName,
    string MiddleName,
    string LastName,
    string Suffix,
    string Email,
    string MobileNumber,
    string Address,
    DateTime? Birthday,
    string BirthPlace,
    string Gender,
    string CivilStatus,
    string Nationality,
    string FatherName,
    string MotherName,
    IFormFile? ProfilePicture = null,
    IFormFile? IdentificationFront = null,
    IFormFile? IdentificationBack = null)
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            var resident = await _context.Residences.FirstOrDefaultAsync(r => r.Email == userEmail);
            if (resident == null || resident.ResidentID != ResidentID)
                return RedirectToAction("Index");

            // Buuin ang FullName (with suffix kung meron)
            string fullName = string.IsNullOrWhiteSpace(MiddleName)
                ? $"{FirstName} {LastName}"
                : $"{FirstName} {MiddleName} {LastName}";
            if (!string.IsNullOrWhiteSpace(Suffix))
                fullName += $" {Suffix}";

            // I-update lahat ng fields
            resident.Firstname = FirstName;
            resident.Middlename = MiddleName;
            resident.Lastname = LastName;  
            resident.Suffix = Suffix;
            resident.FullName = fullName;
            resident.Email = Email;
            resident.MobileNumber = MobileNumber;
            resident.Address = Address;
            resident.Birthday = Birthday;
            resident.BirthPlace = BirthPlace;
            resident.Gender = Gender;
            resident.CivilStatus = CivilStatus;
            resident.Nationality = string.IsNullOrWhiteSpace(Nationality) ? "Filipino" : Nationality;
            resident.FatherName = FatherName;
            resident.MotherName = MotherName;
            resident.UpdatedAt = DateTime.Now;

            // Profile Picture (kung may in-upload)
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                using var ms = new MemoryStream();
                await ProfilePicture.CopyToAsync(ms);
                resident.Picture = ms.ToArray();
            }

            // ID Front (kung may in-upload)
            if (IdentificationFront != null && IdentificationFront.Length > 0)
            {
                using var ms = new MemoryStream();
                await IdentificationFront.CopyToAsync(ms);
                resident.IdentificationFront = ms.ToArray();
            }

            // ID Back (kung may in-upload)
            if (IdentificationBack != null && IdentificationBack.Length > 0)
            {
                using var ms = new MemoryStream();
                await IdentificationBack.CopyToAsync(ms);
                resident.IdentificationBack = ms.ToArray();
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            TempData["IsProfileUpdate"] = true;

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAppointment(ResidentDashboardViewModel model,
            string? RelativeFirstname, string? RelativeLastname, string? RelativeRelationship,
            string? RelativeMobileNumber, string? RelativeEmail, string? RelativeAddress)
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            var resident = _context.Residences.FirstOrDefault(r => r.Email == userEmail);
            var appt = model.NewAppointment;

            appt.ControlNo = "APP-" + new Random().Next(1000, 9999);
            appt.Status = "PENDING";
            appt.PaymentStatus = "UNPAID";
            appt.DateSubmitted = DateTime.Now;
            appt.AppointmentType = "Online";

            if (string.IsNullOrEmpty(appt.FullAddress) && resident != null)
            {
                appt.FullAddress = resident.Address ?? string.Empty;
                appt.FullName = resident.FullName ?? string.Empty;
                appt.EmailAddress = resident.Email ?? string.Empty;
                appt.PhoneNumber = resident.MobileNumber ?? string.Empty;
            }

            if (appt.RequestFor != "Self" && !string.IsNullOrEmpty(RelativeFirstname))
            {
                var newRelative = new RelativesModel
                {
                    UserID = appt.ResidentID,
                    Firstname = RelativeFirstname,
                    Lastname = RelativeLastname,
                    Relationship = RelativeRelationship,
                    MobileNumber = RelativeMobileNumber,
                    Email = RelativeEmail,
                    Address = RelativeAddress
                };

                _context.Relatives.Add(newRelative);
                await _context.SaveChangesAsync();
            }

            _context.Appointments.Add(appt);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Appointment Submitted Successfully!";
            TempData["ControlNo"] = appt.ControlNo;

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitComplaint(
          int ResidentID,
          string ComplaintType,
          string Title,
          string Description,
          string LocationDetails,
          string Latitude,
          string Longitude,
          bool IsAnonymous,
          IFormFile? ComplaintPhoto)
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            var resident = await _context.Residences.FirstOrDefaultAsync(r => r.Email == userEmail);
            if (resident == null)
                return RedirectToAction("Login", "Account");

            string controlNo = "CMP-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string? photoPath = null;

            if (ComplaintPhoto != null && ComplaintPhoto.Length > 0)
            {
                string uploadFolder = Path.Combine(_env.WebRootPath ?? "", "uploads", "complaints");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ComplaintPhoto.FileName);
                string fullPath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ComplaintPhoto.CopyToAsync(stream);
                }
                photoPath = "/uploads/complaints/" + fileName;
            }

            var complaint = new Complaint
            {
                ControlNo = controlNo,
                ResidentID = resident.ResidentID,
                ComplaintType = ComplaintType,
                Title = Title,
                Description = Description,
                LocationDetails = LocationDetails,
                IsAnonymous = IsAnonymous,
                Status = "Pending",
                DateSubmitted = DateTime.Now
            };

            if (!string.IsNullOrWhiteSpace(Latitude) && decimal.TryParse(Latitude, out var lat))
                complaint.Latitude = lat;
            if (!string.IsNullOrWhiteSpace(Longitude) && decimal.TryParse(Longitude, out var lng))
                complaint.Longitude = lng;

            if (!string.IsNullOrEmpty(photoPath))
                complaint.PhotoPath = photoPath;

            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Complaint Submitted Successfully!";
            TempData["IsComplaint"] = true;
            TempData["ControlNo"] = controlNo;

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");

            if (!string.IsNullOrEmpty(userEmail))
            {
                try
                {
                    var resident = await _context.Residences.FirstOrDefaultAsync(r => r.Email == userEmail);
                    if (resident != null)
                    {
                        resident.AccountStatus = "Offline";
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to set resident status to Offline during logout.");
                }
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}