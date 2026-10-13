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
        public DbSet<Residence> Residences { get; set; } = null!;
        public DbSet<AppointmentModel> Appointments { get; set; } = null!;
        public DbSet<DocumentServiceModel> DocumentServices { get; set; } = null!;
        public DbSet<ServiceDetailModel> ServiceDetails { get; set; } = null!;
        public DbSet<RelativesModel> Relatives { get; set; } = null!;
        public DbSet<Complaint> Complaints { get; set; } = null!;
        public DbSet<SatelliteOffice> SatelliteOffices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppointmentModel>().ToTable("appointments");

            // ControlNo is UNIQUE in the database
            modelBuilder.Entity<AppointmentModel>()
                .HasIndex(a => a.ControlNo)
                .IsUnique();

            // Let MySQL fill these timestamps
            modelBuilder.Entity<AppointmentModel>()
                .Property(a => a.CreatedAt)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppointmentModel>()
                .Property(a => a.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}