using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoorGeneralHospital.Models.InputDTO
{
    public class RejectionInput
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Rejection Reason")]
        public string Reason { get; set; }
    }
}