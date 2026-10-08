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

        // === DATABASE TABLES / DBSETS ===
        public DbSet<Residence> Residences { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<DocumentServiceModel> DocumentServices { get; set; }
        public DbSet<ServiceDetailModel> ServiceDetails { get; set; }
        public DbSet<RelativesModel> Relatives { get; set; }
        public DbSet<Complaint> Complaints { get; set; }

    }
}