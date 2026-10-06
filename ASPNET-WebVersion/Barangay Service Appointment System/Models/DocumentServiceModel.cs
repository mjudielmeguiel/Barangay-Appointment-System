using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barangay_Service_Appointment_System.Models
{
    [Table("document_services")]
    public class DocumentServiceModel
    {
        [Key]
        public int ID { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsActive { get; set; }
    }
}