using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Barangay_Service_Appointment_System.Models
{
    [Table("appointments")]
    public class AppointmentModel
    {
        [Key]
        public int AppointmentID { get; set; }

        public string? ControlNo { get; set; }

        public int ResidentID { get; set; }

        // ===== Personal info (auto-filled from resident) =====
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string FullAddress { get; set; } = string.Empty;

        public string IDNumber { get; set; } = string.Empty;

        // ===== Appointment details =====
        [Required]
        public string RequestType { get; set; } = string.Empty;

        [Required]
        public string Purpose { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        public DateTime AppointmentDate { get; set; } = DateTime.Today;

        // enum column in MySQL (e.g. 'Walk-in') -> mapped as string
        public string AppointmentType { get; set; } = "Online";

        public string Status { get; set; } = "PENDING";

        public string? RequestFor { get; set; }

        public string? RepresentativeName { get; set; }

        public string? SenderName { get; set; }

        // ===== Payment =====
        public decimal? Amount { get; set; }

        public string PaymentStatus { get; set; } = "UNPAID";

        public DateTime? PaymentDate { get; set; }

        public string? OfficialReceiptNo { get; set; }

        public string? TransactionNumber { get; set; }

        public string? WalletUsed { get; set; }

        // ===== Scheduling / processing =====
        public DateTime? ScheduledDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PickupDate { get; set; }

        public TimeSpan? PickupTime { get; set; }

        public string? ProcessedBy { get; set; }

        // ===== Cancellation =====
        public string? CancellationReason { get; set; }

        public string? CancelledBy { get; set; }

        // ===== Timestamps =====
        public DateTime? DateSubmitted { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        // ===== Binary files stored in DB (longblob) =====
        public byte[]? AuthorizationLetter { get; set; }

        public byte[]? ProofDocumentBytes { get; set; }

        public byte[]? RepresentativeIDCard { get; set; }

        // ===== Upload helpers (NOT saved to DB) =====
        [NotMapped]
        public IFormFile? ProofDocument { get; set; }

        [NotMapped]
        public IFormFile? AuthorizationLetterFile { get; set; }

        [NotMapped]
        public IFormFile? RepresentativeIDFile { get; set; }
    }
}