using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models.Sp_Model
{
    public class DoctorSchedule_GetDoctorScheduleDetails
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Speciality { get; set; }
        public string DoctorName { get; set; }
        public string AvailableDays { get; set; }
        public string AvailableTime { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

    }
}