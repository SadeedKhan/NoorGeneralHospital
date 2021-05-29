using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models
{
    public class Activities
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public string AppointmentDate { get; set; }
    }
}