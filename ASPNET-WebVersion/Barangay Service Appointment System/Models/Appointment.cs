using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barangay_Service_Appointment_System.Models
{
    [Table("appointments")]
    public class AppointmentModel
    {
        [Key]
        public int AppointmentID { get; set; }
        public int ResidentID { get; set; }
        public string? ControlNo { get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullAddress { get; set; }
        public string? RequestType { get; set; }
        public string? Purpose { get; set; }
        public string? Department { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? AppointmentType { get; set; }

        // Idinagdag ang mga ito para mawala ang error sa HomeController.cs
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? DateSubmitted { get; set; }
    }
}