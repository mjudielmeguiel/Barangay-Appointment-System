using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barangay_Service_Appointment_System.Models
{
    [Table("service_details")]
    public class ServiceDetailModel
    {
        [Key]
        public int ID { get; set; }
        public int? Service_ID { get; set; }
        public string? Purpose { get; set; }
        public string? Description { get; set; }
    }
}