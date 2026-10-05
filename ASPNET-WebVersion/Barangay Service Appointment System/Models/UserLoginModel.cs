using System.ComponentModel.DataAnnotations;

namespace AppointmentSystem.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }

    public class UserSessionModel
    {
        public int ResidentID { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? DepartmentID { get; set; }
        public string? AccountStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}