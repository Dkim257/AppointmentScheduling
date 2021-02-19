using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppointmentScheduling.Models.ViewModels;

namespace AppointmentScheduling.Services
{
    public interface IAppointmentService
    {
        public List<DoctorViewModel> GetDoctorList();
        public List<PatientViewModel> GetPatientList();
        public Task<int> AddUpdate(AppointmentViewModel model);
        public List<AppointmentViewModel> DoctorsEventsById(string dId);
        public List<AppointmentViewModel> PatientsEventsById(string pId);
        public AppointmentViewModel GetById(int id);
        public Task<int> ConfirmEvent(int id);
        public Task<int> Delete(int id);
    }
}
