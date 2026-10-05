using AppointmentSystem.Models;
using Barangay_Service_Appointment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Barangay_Service_Appointment_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT ResidentID, Firstname, Middlename, Lastname, Username, Email, DepartmentID, AccountStatus, CreatedAt FROM residences WHERE Username = @Username AND Password = @Password";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", model.Username);
                    cmd.Parameters.AddWithValue("@Password", model.Password);

                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["Firstname"]?.ToString() ?? string.Empty;
                            string middleName = reader["Middlename"]?.ToString() ?? string.Empty;
                            string lastName = reader["Lastname"]?.ToString() ?? string.Empty;
                            string fullName = $"{firstName} {middleName} {lastName}".Trim();

                            string emailValue = reader["Email"] != DBNull.Value ? reader["Email"].ToString() ?? string.Empty : string.Empty;

                            // Session Mapping para tumugma sa HomeController.cs
                            HttpContext.Session.SetString("UserEmail", emailValue);
                            HttpContext.Session.SetString("Username", reader["Username"]?.ToString() ?? string.Empty);
                            HttpContext.Session.SetString("FullName", string.IsNullOrEmpty(fullName) ? "N/A" : fullName);
                            HttpContext.Session.SetString("AccountStatus", reader["AccountStatus"] != DBNull.Value ? reader["AccountStatus"].ToString() ?? "Unknown" : "Unknown");

                            string createdAtStr = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]).ToString("MMM dd, yyyy") : DateTime.Now.ToString("MMM dd, yyyy");
                            HttpContext.Session.SetString("CreatedAt", createdAtStr);

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                            return View(model);
                        }
                    }
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}