using Microsoft.EntityFrameworkCore;
using Barangay_Service_Appointment_System.Models;

namespace Barangay_Service_Appointment_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Residence> Residences { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
    }
}