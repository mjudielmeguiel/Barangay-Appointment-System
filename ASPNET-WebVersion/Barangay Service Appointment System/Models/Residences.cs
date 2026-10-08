using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barangay_Service_Appointment_System.Models
{
    [Table("residences")]
    public class Residence
    {
        [Key]
        public int ResidentID { get; set; }

        public string? ResidentCode { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public string? Suffix { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string? BirthPlace { get; set; }
        public string? Gender { get; set; }
        public string? CivilStatus { get; set; }
        public string? Nationality { get; set; }
        public string? AccountStatus { get; set; }
        public byte[]? Picture { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public byte[]? IdentificationFront { get; set; }
        public byte[]? IdentificationBack { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Iba pang fields mula sa table mo — kung kailangan
        public int? DepartmentID { get; set; }
        public string? SatelliteOffice { get; set; }
        public int? SatelliteOfficeID { get; set; }
        public string? Password { get; set; }
        public DateTime? LockoutExpiry { get; set; }
        public int? LoginAttempts { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? DeleteComment { get; set; }
    }
}