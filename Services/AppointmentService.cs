using System;
using System.Collections.Generic;
using AppointmentScheduling.Models;
using AppointmentScheduling.Models.ViewModels;
using System.Linq;
using AppointmentScheduling.Utilities;

namespace AppointmentScheduling.Services
{ 
    // All of data operations regarding appointment will be done inside this service
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<DoctorViewModel> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorViewModel
                           {
                               Id = user.Id,
                               Name = user.Name
                           }).ToList();

            return doctors;
        }

        public List<PatientViewModel> GetPatientList()
        {
            var patients = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                           select new PatientViewModel
                           {
                               Id = user.Id,
                               Name = user.Name
                           }).ToList();

            return patients;
        }
    }
}
