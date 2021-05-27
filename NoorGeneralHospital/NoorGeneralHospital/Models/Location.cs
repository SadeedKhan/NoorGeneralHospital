using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Location.")]
        public string LocationName { get; set; }
        public string NormalizeLocationName { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
    }
}