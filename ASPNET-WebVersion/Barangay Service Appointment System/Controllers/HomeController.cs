using Barangay_Service_Appointment_System.Data;
using Barangay_Service_Appointment_System.Models;
using Barangay_Service_Appointment_System.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

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

            // Kunin ang buong Document Services kung saan IsActive == true
            var documentServicesList = _context.DocumentServices
                .Where(ds => ds.IsActive == true)
                .ToList();

            // Kunin ang buong Service Details (may kasamang Service_ID at Purpose)
            var serviceDetailsList = _context.ServiceDetails
                .ToList();

            var savedRelatives = _context.Relatives
                .Where(r => r.UserID == resident.ResidentID)
                .ToList();

            var viewModel = new ResidentDashboardViewModel
            {
                Resident = resident,
                Appointments = appointments,
                DocumentServicesList = documentServicesList,
                ServiceDetailsList = serviceDetailsList,
                SavedRelatives = savedRelatives
            };

            return View(viewModel);
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

            // Siguraduhing may laman ang FullAddress galing sa Resident profile kung sakaling null
            if (string.IsNullOrEmpty(appt.FullAddress) && resident != null)
            {
                appt.FullAddress = resident.Address;
                appt.FullName = resident.FullName;
                appt.EmailAddress = resident.Email;
                appt.PhoneNumber = resident.MobileNumber;
            }

            // Kung hindi "Self" ang pinili, i-save ang impormasyon sa Relatives table
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