using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models
{
    public class Status
    {
        public int? Id { get; set; }
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
    }
}