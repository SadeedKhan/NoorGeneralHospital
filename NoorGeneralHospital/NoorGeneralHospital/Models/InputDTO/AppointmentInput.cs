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
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Patient Name.")]
        public string PatientName { get; set; }
        [Required(ErrorMessage = "Please Enter Patient Age.")]
        public int PatientAge { get; set; }
        [Required(ErrorMessage = "Please Enter Patient Email.")]
        public string PatientEmail { get; set; }
        [Required(ErrorMessage = "Please Enter Patient Phone.")]
        public string PatientPhone { get; set; }
        [Required(ErrorMessage = "Please Select Speciality")]
        public int SpecialityId { get; set; }
        [Required(ErrorMessage = "Please Select Doctor.")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Please Select Doctor.")]
        public string AppointmentDate { get; set; }
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