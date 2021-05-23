using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models.Sp_Model
{
    public class Dashboard_DashboardStats
    {
        public int Doctors { get; set; }
        public int TotalAppointments { get; set; }
        public int Attend { get; set; }
        public int Pending { get; set; }
    }
}