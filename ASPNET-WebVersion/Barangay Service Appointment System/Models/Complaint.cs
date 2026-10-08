using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barangay_Service_Appointment_System.Models
{
    [Table("complaints")]
    public class Complaint
    {
        [Key]
        [Column("ComplaintID")]
        public int ID { get; set; }

        [Column("ControlNo")]
        public string ControlNo { get; set; } = string.Empty;

        [Column("AssignedStaffID")]
        public int? AssignedStaffID { get; set; }

        [Column("ComplaintType")]
        public string ComplaintType { get; set; } = string.Empty;

        [Column("Title")]
        public string Title { get; set; } = string.Empty;

        [Column("Description")]
        public string Description { get; set; } = string.Empty;

        [Column("LocationDetails")]
        public string? LocationDetails { get; set; }

        [Column("Latitude")]
        public decimal? Latitude { get; set; }

        [Column("Longitude")]
        public decimal? Longitude { get; set; }

        [Column("PhotoPath")]
        public string? PhotoPath { get; set; }

        [Column("IsAnonymous")]
        public bool IsAnonymous { get; set; } = false;

        [Column("ResidentID")]
        public int ResidentID { get; set; } // ✅ Dapat HINDI nullable — tugma sa DB na NOT NULL

        [Column("Status")]
        public string Status { get; set; } = "Pending";

        [Column("StaffRemarks")]
        public string? StaffRemarks { get; set; }

        [Column("DateSubmitted")]
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        [Column("DateUpdated")]
        public DateTime? DateUpdated { get; set; }
    }
}