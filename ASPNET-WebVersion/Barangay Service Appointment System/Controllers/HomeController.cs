using Barangay_Service_Appointment_System.Models;
using Barangay_Service_Appointment_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using System;

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

            return View(resident);
        }

        [HttpGet]
        public IActionResult CreateAppointment()
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

            var appointment = new AppointmentModel
            {
                ResidentID = resident.ResidentID,
                FullName = resident.FullName,
                EmailAddress = resident.Email,
                PhoneNumber = resident.MobileNumber,
                FullAddress = resident.Address
            };

            return View(appointment);
        }

        [HttpPost]
        public IActionResult CreateAppointment(AppointmentModel model)
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            model.ControlNo = "BP-" + DateTime.Now.ToString("yyyyMMdd") + "-" + new Random().Next(1000, 9999);
            model.Status = "PENDING";
            model.PaymentStatus = "UNPAID";
            model.DateSubmitted = DateTime.Now;

            // Alisin ang comment sa ibaba kapag naidagdag na ang DbSet<AppointmentModel> sa ApplicationDbContext
            // _context.Appointments.Add(model);
            // _context.SaveChanges();

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