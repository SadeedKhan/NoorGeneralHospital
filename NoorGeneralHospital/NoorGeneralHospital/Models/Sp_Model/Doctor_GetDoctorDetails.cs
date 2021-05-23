using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models.Sp_Model
{
    public class Doctor_GetDoctorDetails
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int SpecialityId { get; set; }
        public string Speciality { get; set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public string Services { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string ImagePath { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string ShortBioGraphy { get; set; }
        public bool IsActive { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}