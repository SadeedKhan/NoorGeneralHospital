using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int SpecialityId { get; set; }
        public int GenderId { get; set; }
        public int LocationId { get; set; }
        public string Services { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string ImagePath { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string ShortBioGraphy { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
    }
}