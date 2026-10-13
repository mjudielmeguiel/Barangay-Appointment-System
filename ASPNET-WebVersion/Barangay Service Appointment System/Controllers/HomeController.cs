using Barangay_Service_Appointment_System.Data;
using Barangay_Service_Appointment_System.Models;
using Barangay_Service_Appointment_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Barangay_Service_Appointment_System.Controllers
{
    public class HomeController : Controller
    {
        private const int MaxUploadBytes = 5 * 1024 * 1024; // raw upload limit
        private const int AvatarSize = 600;                  // stored avatar is 600x600
        private const long MaxPixels = 40_000_000;           // about 40 megapixels

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        private async Task<string> GenerateResidentCodeAsync()
        {
            var codes = await _context.Residences
                .Where(r => r.ResidentCode != null && r.ResidentCode.StartsWith("RES-"))
                .Select(r => r.ResidentCode!)
                .ToListAsync();

            int max = 0;
            foreach (var code in codes)
            {
                if (int.TryParse(code.Substring(4), out int n) && n > max)
                    max = n;
            }
            return "RES-" + (max + 1).ToString("D3");
        }

        private async Task<(byte[]? Bytes, string? Error)> ProcessProfilePictureAsync(IFormFile file)
        {
            if (file.Length > MaxUploadBytes)
                return (null, "The photo is too large. Maximum is 5MB.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using var inputStream = new SKManagedStream(stream);
                using var original = SKBitmap.Decode(inputStream);

                if (original == null)
                    return (null, "That file is not a valid image. Please upload a JPG or PNG.");

                if ((long)original.Width * original.Height > MaxPixels)
                    return (null, "The photo resolution is too high. Please use a smaller image.");

                // Crop image to a perfect square from the center
                int size = Math.Min(original.Width, original.Height);
                int x = (original.Width - size) / 2;
                int y = (original.Height - size) / 2;
                using var cropped = new SKBitmap(size, size);
                original.ExtractSubset(cropped, new SKRectI(x, y, x + size, y + size));

                // Resize the cropped square to 600x600
                using var resized = cropped.Resize(new SKImageInfo(AvatarSize, AvatarSize), SKSamplingOptions.Default);

                // Draw onto a white background (prevents transparent PNGs from turning black)
                using var surface = SKSurface.Create(new SKImageInfo(AvatarSize, AvatarSize));
                var canvas = surface.Canvas;
                canvas.Clear(SKColors.White);

                canvas.DrawBitmap(resized, 0, 0, SKSamplingOptions.Default);

                // Encode as JPEG at 85 quality
                using var image = surface.Snapshot();
                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 85);

                return (data.ToArray(), null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Profile picture processing failed");
                return (null, "The photo could not be processed. Please try another image.");
            }
        }

        private async Task<(byte[]? Bytes, string? Error)> ProcessIdImageAsync(IFormFile file)
        {
            if (file.Length > MaxUploadBytes)
                return (null, "The ID photo is too large. Maximum is 5MB.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using var inputStream = new SKManagedStream(stream);
                using var original = SKBitmap.Decode(inputStream);

                if (original == null)
                    return (null, "That file is not a valid image. Please upload a JPG or PNG.");

                if ((long)original.Width * original.Height > MaxPixels)
                    return (null, "The photo resolution is too high. Please use a smaller image.");

                // For IDs, we do NOT crop to a square. We resize maintaining the aspect ratio.
                // Max 1200px width/height to keep text highly readable.
                int maxSize = 1200;
                int newWidth = original.Width;
                int newHeight = original.Height;

                if (original.Width > maxSize || original.Height > maxSize)
                {
                    if (original.Width > original.Height)
                    {
                        newWidth = maxSize;
                        newHeight = (int)((float)original.Height / original.Width * maxSize);
                    }
                    else
                    {
                        newHeight = maxSize;
                        newWidth = (int)((float)original.Width / original.Height * maxSize);
                    }
                }

                using var resized = original.Resize(new SKImageInfo(newWidth, newHeight), SKSamplingOptions.Default);

                // Draw onto a white background (prevents transparent PNGs from turning black)
                using var surface = SKSurface.Create(new SKImageInfo(newWidth, newHeight));
                var canvas = surface.Canvas;
                canvas.Clear(SKColors.White);

                canvas.DrawBitmap(resized, 0, 0, SKSamplingOptions.Default);

                // Encode as JPEG at 85 quality
                using var image = surface.Snapshot();
                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 85);

                return (data.ToArray(), null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID picture processing failed");
                return (null, "The ID photo could not be processed. Please try another image.");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            var resident = await _context.Residences.FirstOrDefaultAsync(r => r.Email == userEmail);
            if (resident == null)
                return NotFound("Resident profile not found.");

            if (string.IsNullOrWhiteSpace(resident.ResidentCode))
            {
                try
                {
                    resident.ResidentCode = await GenerateResidentCodeAsync();
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to generate Resident Code.");
                }
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

            var satelliteOffices = _context.SatelliteOffices
                .OrderBy(o => o.facility_name)
                .ToList();

            // Admin calendar holidays (category 'Holiday').
            var holidayDates = _context.Database
                .SqlQueryRaw<string>(
                    "SELECT DATE_FORMAT(EventDate, '%Y-%m-%d') AS Value " +
                    "FROM calendar_events WHERE Category = 'Holiday'")
                .ToList();

            ViewBag.HolidayDates = holidayDates;

            var viewModel = new ResidentDashboardViewModel
            {
                Resident = resident,
                Appointments = appointments,
                Complaints = complaints,
                DocumentServicesList = documentServicesList,
                ServiceDetailsList = serviceDetailsList,
                SavedRelatives = savedRelatives,
                SatelliteOffices = satelliteOffices
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAppointment(ResidentDashboardViewModel model)
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            var newAppt = model.NewAppointment;

            if (newAppt != null)
            {
                try
                {
                    newAppt.ControlNo = $"APP-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";
                    newAppt.DateSubmitted = DateTime.Now;
                    newAppt.Status = "Pending";
                    newAppt.AppointmentType = "Online";

                    _context.Appointments.Add(newAppt);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Your appointment request has been successfully submitted and is awaiting approval.";
                    TempData["ControlNo"] = newAppt.ControlNo;
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    _logger.LogError(ex, "Failed to save appointment request: {Message}", errorMsg);
                    TempData["ErrorMessage"] = $"Error: {errorMsg}";
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitComplaint(
            int ResidentID, string ComplaintType, string Title, string Description,
            string LocationDetails, bool IsAnonymous)
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            try
            {
                var complaint = new Complaint
                {
                    ResidentID = ResidentID,
                    ComplaintType = ComplaintType,
                    Title = Title,
                    Description = Description,
                    LocationDetails = LocationDetails,
                    IsAnonymous = IsAnonymous,
                    ControlNo = $"CMP-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}",
                    DateSubmitted = DateTime.Now,
                    Status = "Pending"
                };

                _context.Complaints.Add(complaint);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Your complaint has been safely recorded and will be reviewed by the barangay.";
                TempData["ControlNo"] = complaint.ControlNo;
                TempData["IsComplaint"] = true;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, "Failed to save complaint: {Message}", errorMsg);
                TempData["ErrorMessage"] = $"Error: {errorMsg}";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetIdentificationFront()
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return NotFound();

            var picture = await _context.Residences
                .AsNoTracking()
                .Where(r => r.Email == userEmail)
                .Select(r => r.IdentificationFront)
                .FirstOrDefaultAsync();

            if (picture == null || picture.Length == 0)
                return NotFound(); // Return 404 so the frontend hides the broken image icon

            string contentType = picture.Length > 4 && picture[0] == 0x89 && picture[1] == 0x50 && picture[2] == 0x4E && picture[3] == 0x47 ? "image/png" : "image/jpeg";
            Response.Headers.CacheControl = "private, max-age=86400";
            return File(picture, contentType);
        }

        [HttpGet]
        public async Task<IActionResult> GetIdentificationBack()
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return NotFound();

            var picture = await _context.Residences
                .AsNoTracking()
                .Where(r => r.Email == userEmail)
                .Select(r => r.IdentificationBack)
                .FirstOrDefaultAsync();

            if (picture == null || picture.Length == 0)
                return NotFound();

            string contentType = picture.Length > 4 && picture[0] == 0x89 && picture[1] == 0x50 && picture[2] == 0x4E && picture[3] == 0x47 ? "image/png" : "image/jpeg";
            Response.Headers.CacheControl = "private, max-age=86400";
            return File(picture, contentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(
            int ResidentID,
            string FirstName,
            string? MiddleName,
            string LastName,
            string? Suffix,
            string Email,
            string MobileNumber,
            string Address,
            DateTime? Birthday,
            string BirthPlace,
            string Gender,
            string CivilStatus,
            string? Nationality,
            string? FatherName,
            string? MotherName,
            IFormFile? ProfilePicture,
            IFormFile? IdentificationFront,
            IFormFile? IdentificationBack)
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            var resident = await _context.Residences.FirstOrDefaultAsync(r => r.Email == userEmail);
            if (resident == null || resident.ResidentID != ResidentID)
                return RedirectToAction("Index");

            // Process Profile Picture
            byte[]? newPicture = null;
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                var (bytes, error) = await ProcessProfilePictureAsync(ProfilePicture);
                if (bytes == null)
                {
                    TempData["ErrorMessage"] = error;
                    return RedirectToAction("Index");
                }
                newPicture = bytes;
            }

            // Process Identification Front
            if (IdentificationFront != null && IdentificationFront.Length > 0)
            {
                var (bytes, error) = await ProcessIdImageAsync(IdentificationFront);
                if (bytes == null)
                {
                    TempData["ErrorMessage"] = "Front ID Error: " + error;
                    return RedirectToAction("Index");
                }
                resident.IdentificationFront = bytes;
            }

            // Process Identification Back
            if (IdentificationBack != null && IdentificationBack.Length > 0)
            {
                var (bytes, error) = await ProcessIdImageAsync(IdentificationBack);
                if (bytes == null)
                {
                    TempData["ErrorMessage"] = "Back ID Error: " + error;
                    return RedirectToAction("Index");
                }
                resident.IdentificationBack = bytes;
            }

            // Update Text Data
            string fullName = string.IsNullOrWhiteSpace(MiddleName)
                ? $"{FirstName} {LastName}"
                : $"{FirstName} {MiddleName} {LastName}";
            if (!string.IsNullOrWhiteSpace(Suffix))
                fullName += $" {Suffix}";

            resident.Firstname = FirstName;
            resident.Middlename = MiddleName ?? string.Empty;
            resident.Lastname = LastName;
            resident.Suffix = Suffix ?? string.Empty;
            resident.FullName = fullName;
            resident.Email = Email;
            resident.MobileNumber = MobileNumber;
            resident.Address = Address;
            resident.Birthday = Birthday;
            resident.BirthPlace = BirthPlace;
            resident.Gender = Gender;
            resident.CivilStatus = CivilStatus;
            resident.Nationality = string.IsNullOrWhiteSpace(Nationality) ? "Filipino" : Nationality;
            resident.FatherName = FatherName ?? string.Empty;
            resident.MotherName = MotherName ?? string.Empty;
            resident.UpdatedAt = DateTime.Now;

            if (newPicture != null)
            {
                resident.Picture = newPicture;
                resident.ProfileImagePath = null;
            }

            try
            {
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("UserEmail", resident.Email ?? string.Empty);

                TempData["SuccessMessage"] = "Profile updated successfully!";
                TempData["IsProfileUpdate"] = true;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.LogError(ex, "Profile update failed: {Message}", errorMsg);
                TempData["ErrorMessage"] = $"Error: {errorMsg}";
            }

            return RedirectToAction("Index");
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
                    _logger.LogError(ex, "Logout status update failed");
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

        [HttpGet]
        public async Task<IActionResult> GetProfilePicture()
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return Redirect("~/images/default-profile.png");

            var picture = await _context.Residences
                .AsNoTracking()
                .Where(r => r.Email == userEmail)
                .Select(r => r.Picture)
                .FirstOrDefaultAsync();

            if (picture == null || picture.Length == 0)
                return Redirect("~/images/default-profile.png");

            string contentType =
                picture.Length > 4 && picture[0] == 0x89 && picture[1] == 0x50 && picture[2] == 0x4E && picture[3] == 0x47
                    ? "image/png"
                    : "image/jpeg";

            Response.Headers.CacheControl = "private, max-age=86400";
            return File(picture, contentType);
        }
    }
}