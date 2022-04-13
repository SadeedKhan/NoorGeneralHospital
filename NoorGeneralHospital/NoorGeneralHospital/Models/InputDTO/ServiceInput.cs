using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Models.InputDTO
{
    public class ServiceInput
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Service Title.")]
        public string ServiceTitle { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [AllowHtml]
        public HttpPostedFileWrapper file { get; set; }
    }
}