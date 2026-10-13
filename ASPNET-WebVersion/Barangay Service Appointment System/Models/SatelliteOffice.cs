using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barangay_Service_Appointment_System.Models
{
    [Table("brgy_putatan_satellite_offices")]
    public class SatelliteOffice
    {
        [Key]
        public byte id { get; set; }   // use int if the MySQL column is INT, not TINYINT

        [Required]
        public string facility_name { get; set; } = string.Empty;
    }
}