using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Models.InputDTO
{
    public class DoctorScheduleInput
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Select Doctor")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Must Choose a Date")]
        public int[] AvailableDays { get; set; }
        public string AvailableDay { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public IEnumerable<SelectListItem> GetDay()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> days = context.Days.AsNoTracking()
                    .OrderByDescending(n => n.Id)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.Name
                        }).ToList();
                return new SelectList(days, "Value", "Text");
            }
        }

        public IEnumerable<SelectListItem> GetDoctor()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> doctor = context.Doctors.AsNoTracking()
                    .OrderByDescending(n => n.Id)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.UserName
                        }).ToList();
                var doctortip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Doctor ---"
                };
                doctor.Insert(0, doctortip);
                return new SelectList(doctor, "Value", "Text");
            }
        }
    }
}