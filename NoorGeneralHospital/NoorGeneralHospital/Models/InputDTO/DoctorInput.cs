using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models.InputDTO
{
    public class DoctorInput
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int SpecialityId { get; set; }
        public string Services { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public HttpPostedFileWrapper File { get; set; }
    }
}