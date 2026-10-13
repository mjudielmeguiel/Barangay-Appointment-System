using Barangay_Service_Appointment_System.Models;
using System;
using System.Collections.Generic;

namespace Barangay_Service_Appointment_System.ViewModels
{
    public class ResidentDashboardViewModel
    {
        public Residence Resident { get; set; } = null!;

        public List<AppointmentModel> Appointments { get; set; } = new();
        public List<Complaint> Complaints { get; set; } = new();
        public List<DocumentServiceModel> DocumentServicesList { get; set; } = new();
        public List<ServiceDetailModel> ServiceDetailsList { get; set; } = new();
        public List<RelativesModel> SavedRelatives { get; set; } = new();
        public List<SatelliteOffice> SatelliteOffices { get; set; } = new();   // was DbSet<> (wrong)

        public AppointmentModel NewAppointment { get; set; } = new();
    }
}