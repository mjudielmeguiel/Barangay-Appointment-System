using AppointmentSystem.Models;
using Barangay_Service_Appointment_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;

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

        [HttpPost]
        public IActionResult Register(string Firstname, string Lastname, string Email, string Username, string Password)
        {
            if (string.IsNullOrWhiteSpace(Firstname) || string.IsNullOrWhiteSpace(Lastname) ||
                string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
            {
                TempData["ErrorMessage"] = "Please fill in all required fields.";
                return RedirectToAction("Login", "Account");
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // Awtomatikong pinagsasama ang Firstname at Lastname para sa FullName column
                string fullName = $"{Firstname} {Lastname}".Trim();

                // Pinalitan ang table name papuntang 'residences' at in-update ang mga columns
                string query = @"INSERT INTO residences 
                                 (Firstname, Lastname, FullName, Email, Username, Password, CreatedAt, AccountStatus) 
                                 VALUES (@Firstname, @Lastname, @FullName, @Email, @Username, @Password, NOW(), 'Active')";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Firstname", Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", Lastname);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        TempData["SuccessMessage"] = "Account created successfully! You can now log in.";
                        return RedirectToAction("Index", "Home"); // Palitan ang "Home" kung sa ibang page dapat pumunta
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Number == 1062)
                        {
                            TempData["ErrorMessage"] = "Username or Email is already taken.";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "An error occurred while creating the account.";
                        }

                        return RedirectToAction("Index", "Home");
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