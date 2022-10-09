using NoorGeneralHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Models.InputDTO
{
    public class AppointmentInput
    {
        [Required]
        public int SpecialityId { get; set; }
        [Required(ErrorMessage = "Please Select Doctor.")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please Select Appoinment Date.")]
        public string AppointmentDate { get; set; }

        [Required(ErrorMessage = "Please Select Appoinment Time.")]
        public string AppointmentTime { get; set; }
        public string Description { get; set; }

        public IEnumerable<SelectListItem> GetSpeciality()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> speciality = context.Specialities.AsNoTracking()
                    .OrderBy(n => n.SpecialityName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.SpecialityName
                        }).ToList();
                var Specialitytip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Speciality ---"
                };
                speciality.Insert(0, Specialitytip);
                return new SelectList(speciality, "Value", "Text");
            }
        }
    }
}