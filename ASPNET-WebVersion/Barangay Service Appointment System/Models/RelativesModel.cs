using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barangay_Service_Appointment_System.Models
{
    [Table("relatives")]
    public class RelativesModel
    {
        [Key]
        public int RelativeID { get; set; }
        public int UserID { get; set; }
        public string? Relationship { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Middlename { get; set; }
        public string? Suffix { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? BirthPlace { get; set; }
        public string? CivilStatus { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
    }
}