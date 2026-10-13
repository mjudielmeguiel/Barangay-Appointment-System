using AppointmentSystem.Models;
using Barangay_Service_Appointment_System.Data;
using Barangay_Service_Appointment_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Barangay_Service_Appointment_System.Controllers
{
    public class AccountController : Controller
    {
        private const long MaxProfilePictureBytes = 5 * 1024 * 1024; // 5 MB
        private static readonly string[] AllowedImageExts = { ".jpg", ".jpeg", ".png" };

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env; // ✅ for wwwroot file path

        public AccountController(IConfiguration configuration, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.SatelliteOffices = _context.SatelliteOffices
                .OrderBy(s => s.facility_name)
                .ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SatelliteOffices = _context.SatelliteOffices
                    .OrderBy(s => s.facility_name)
                    .ToList();
                return View(model);
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // ⚠️ PLAINTEXT PASSWORD — see security note at the end of this answer
                string query = "SELECT ResidentID, Firstname, Middlename, Lastname, Username, Email, DepartmentID, AccountStatus, CreatedAt, Picture FROM residences WHERE Username = @Username AND Password = @Password";

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
                            string picturePath = reader["Picture"] != DBNull.Value ? reader["Picture"].ToString() ?? string.Empty : string.Empty;

                            HttpContext.Session.SetString("UserEmail", emailValue);
                            HttpContext.Session.SetString("Username", reader["Username"]?.ToString() ?? string.Empty);
                            HttpContext.Session.SetString("FullName", string.IsNullOrEmpty(fullName) ? "N/A" : fullName);
                            HttpContext.Session.SetString("ProfilePicture", picturePath); // path like /profiles/xxx.jpg
                            HttpContext.Session.SetString("AccountStatus", reader["AccountStatus"] != DBNull.Value ? reader["AccountStatus"].ToString() ?? "Unknown" : "Unknown");

                            string createdAtStr = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]).ToString("MMM dd, yyyy") : DateTime.Now.ToString("MMM dd, yyyy");
                            HttpContext.Session.SetString("CreatedAt", createdAtStr);

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                            ViewBag.SatelliteOffices = _context.SatelliteOffices
                                .OrderBy(s => s.facility_name)
                                .ToList();
                            return View(model);
                        }
                    }
                }
            }
        }

        [HttpPost]
        [RequestSizeLimit(20971520)] // 20 MB hard cap (friendly error fires at 5 MB)
        public async Task<IActionResult> Register(string Firstname, string Lastname, int? SatelliteOfficeId, string Email, string Username, string Password, IFormFile? ProfilePicture)
        {
            try
            {
                IActionResult RegisterError(string message)
                {
                    ViewBag.SatelliteOffices = _context.SatelliteOffices?.OrderBy(s => s.facility_name).ToList() ?? new List<SatelliteOffice>();
                    ViewBag.OpenRegister = true;
                    ViewBag.RegisterError = message;
                    ViewBag.RegFirstname = Firstname;
                    ViewBag.RegLastname = Lastname;
                    ViewBag.RegEmail = Email;
                    ViewBag.RegUsername = Username;
                    ViewBag.SelectedOfficeId = SatelliteOfficeId;
                    return View("Login", new UserLoginModel());
                }

                // 1. Required fields (nullable int = empty dropdown can never crash)
                if (string.IsNullOrWhiteSpace(Firstname) || string.IsNullOrWhiteSpace(Lastname) ||
                    string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) ||
                    string.IsNullOrWhiteSpace(Email) || !SatelliteOfficeId.HasValue)
                {
                    return RegisterError("Please fill in all required fields, including the Satellite Office.");
                }

                // 2. Email format
                try { new System.Net.Mail.MailAddress(Email); }
                catch { return RegisterError("Please enter a valid email address."); }

                // 3. Validate + save profile picture to FILE PATH (wwwroot/profiles), NOT database
                string? pictureRelativePath = null;
                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    if (ProfilePicture.Length > 5 * 1024 * 1024)
                    {
                        double mb = ProfilePicture.Length / 1024.0 / 1024.0;
                        return RegisterError($"Profile picture is too large. Maximum is 5 MB (your file is {mb:F1} MB).");
                    }

                    string? ext = Path.GetExtension(ProfilePicture.FileName)?.ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext) || (ext != ".jpg" && ext != ".jpeg" && ext != ".png"))
                    {
                        return RegisterError("Profile picture must be a JPG or PNG image.");
                    }

                    // Magic-byte check: confirm it is really an image
                    bool isValidImage;
                    using (var stream = ProfilePicture.OpenReadStream())
                    {
                        byte[] header = new byte[8];
                        int read = await stream.ReadAsync(header, 0, header.Length);
                        isValidImage = read >= 4 &&
                            ((header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF) ||
                             (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47));
                    }
                    if (!isValidImage)
                    {
                        return RegisterError("The selected file is not a valid JPG/PNG image.");
                    }

                    // Guard: web root path must exist (if wwwroot folder is missing, this is null)
                    if (string.IsNullOrEmpty(_env?.WebRootPath))
                    {
                        return RegisterError("Server configuration error: wwwroot folder not found. Ask admin to add the wwwroot folder.");
                    }

                    // Save to file system — wrapped so a write-permission error shows in the modal
                    string uploadFolder = Path.Combine(_env.WebRootPath, "profiles");
                    Directory.CreateDirectory(uploadFolder);
                    string safeFileName = Guid.NewGuid().ToString("N") + ext;
                    string fullPath = Path.Combine(uploadFolder, safeFileName);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await ProfilePicture.CopyToAsync(fileStream);
                    }
                    pictureRelativePath = "/profiles/" + safeFileName;
                }

                // 4. Satellite office name
                string satelliteOfficeName = string.Empty;
                var selectedOffice = _context.SatelliteOffices?.FirstOrDefault(o => o.id == SatelliteOfficeId.Value);
                if (selectedOffice != null) satelliteOfficeName = selectedOffice.facility_name;

                // 5. Insert into DB
                string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string fullName = $"{Firstname} {Lastname}".Trim();
                    string query = @"INSERT INTO residences
                (ResidentCode, Firstname, Lastname, FullName, Email, Username, Password, Picture, CreatedAt, AccountStatus, SatelliteOfficeID, SatelliteOffice)
                SELECT CONCAT('RES-', LPAD(COALESCE(MAX(CAST(SUBSTRING(ResidentCode, 5) AS UNSIGNED)), 0) + 1, 3, '0')),
                @Firstname, @Lastname, @FullName, @Email, @Username, @Password, @Picture, NOW(), 'Active', @SatelliteOfficeId, @SatelliteOfficeName
                FROM residences WHERE ResidentCode LIKE 'RES-%'";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Firstname", Firstname);
                        cmd.Parameters.AddWithValue("@Lastname", Lastname);
                        cmd.Parameters.AddWithValue("@FullName", fullName);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Picture", pictureRelativePath ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SatelliteOfficeId", SatelliteOfficeId.Value);
                        cmd.Parameters.AddWithValue("@SatelliteOfficeName", satelliteOfficeName);

                        try
                        {
                            await conn.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
                            TempData["SuccessMessage"] = "Account created successfully! You can now log in.";
                            return RedirectToAction("Login", "Account");
                        }
                        catch (MySqlException ex)
                        {
                            CleanupOrphanFile(pictureRelativePath);
                            if (ex.Number == 1062) return RegisterError("Username or Email is already taken.");
                            return RegisterError("Database error: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch-all: ANY unexpected error shows in the modal instead of crashing the server
                ViewBag.SatelliteOffices = _context?.SatelliteOffices?.OrderBy(s => s.facility_name).ToList() ?? new List<SatelliteOffice>();
                ViewBag.OpenRegister = true;
                ViewBag.RegisterError = "Registration failed: " + ex.Message;
                ViewBag.RegFirstname = Firstname;
                ViewBag.RegLastname = Lastname;
                ViewBag.RegEmail = Email;
                ViewBag.RegUsername = Username;
                ViewBag.SelectedOfficeId = SatelliteOfficeId;
                return View("Login", new UserLoginModel());
            }
        }

        private void CleanupOrphanFile(string? pictureRelativePath)
        {
            if (pictureRelativePath == null || _env?.WebRootPath == null) return;
            try
            {
                string fullPath = Path.Combine(_env.WebRootPath, pictureRelativePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
            }
            catch { /* best-effort */ }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}