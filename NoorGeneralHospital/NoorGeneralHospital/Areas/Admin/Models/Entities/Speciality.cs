using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Areas.Admin.Models.Entities
{
    public class Speciality
    {
        public Guid? Id { get; set; }
        public string SpecialityName { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public Speciality()
        {
            CreatedOn = DateTime.Now;
            IsActive = true;
        }
    }
}