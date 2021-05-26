using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientAge { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhone { get; set; }
        public int SpecialityId { get; set; }
        public int DoctorId { get; set; }
        public string AppointmentDate { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public int StatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
    }
}