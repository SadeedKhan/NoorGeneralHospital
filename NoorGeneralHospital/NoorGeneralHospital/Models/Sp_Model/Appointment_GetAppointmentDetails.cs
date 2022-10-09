using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models.Sp_Model
{
    public class Appointment_GetAppointmentDetails
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhone { get; set; }
        public int SpecialityId { get; set; }
        public string Speciality { get; set; }
        public int DoctorId { get; set; }
        public string Doctor { get; set; }
        public string ImagePath { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
    }
}