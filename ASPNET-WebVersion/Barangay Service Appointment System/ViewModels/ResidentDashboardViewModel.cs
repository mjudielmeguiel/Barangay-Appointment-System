using System.Collections.Generic;
using Barangay_Service_Appointment_System.Models;

namespace Barangay_Service_Appointment_System.ViewModels
{
    public class ResidentDashboardViewModel
    {
        public Residence? Resident { get; set; }
        public List<AppointmentModel> Appointments { get; set; } = new List<AppointmentModel>();

        // Mga listahan para sa ID-based filtering
        public List<DocumentServiceModel> DocumentServicesList { get; set; } = new List<DocumentServiceModel>();
        public List<ServiceDetailModel> ServiceDetailsList { get; set; } = new List<ServiceDetailModel>();

        public List<RelativesModel> SavedRelatives { get; set; } = new List<RelativesModel>();
        public AppointmentModel NewAppointment { get; set; } = new AppointmentModel();
    }
}