using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AppointmentSystem.Models;

namespace AppointmentSystem.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            // Check session authentication
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = new UserSessionModel
            {
                Username = HttpContext.Session.GetString("Username"),
                FullName = HttpContext.Session.GetString("FullName"),
                Email = HttpContext.Session.GetString("Email"),
                AccountStatus = HttpContext.Session.GetString("AccountStatus"),
                CreatedAt = Convert.ToDateTime(HttpContext.Session.GetString("CreatedAt"))
            };

            return View(user);
        }
    }
}