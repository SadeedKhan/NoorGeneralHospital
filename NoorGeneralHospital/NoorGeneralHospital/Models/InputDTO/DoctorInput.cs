using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Models.InputDTO
{
    public class DoctorInput
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter User Name.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Email.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Phone.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Select Speciality.")]
        public int SpecialityId { get; set; }
        [Required(ErrorMessage = "Please Select Gender.")]
        public int GenderId { get; set; }
        [Required(ErrorMessage = "Please Select Location.")]
        public int LocationId { get; set; }
        [Required(ErrorMessage = "Please Enter Services.")]
        public string Services { get; set; }
        [Required(ErrorMessage = "Please Enter Education.")]
        public string Education { get; set; }
        [Required(ErrorMessage = "Please Enter Experience.")]
        public string Experience { get; set; }
        [Required(ErrorMessage = "Please Select DOB.")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Please Enter Address.")]
        public string Address { get; set; }
        public string ShortBioGraphy { get; set; }
        public bool IsActive { get; set; }
        [AllowHtml]
        public HttpPostedFileWrapper file { get; set; }

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

        public IEnumerable<SelectListItem> GetLocation()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> location = context.Locations.AsNoTracking()
                    .OrderBy(n => n.LocationName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.LocationName
                        }).ToList();
                var Locationtip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Location ---"
                };
                location.Insert(0, Locationtip);
                return new SelectList(location, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> GetGender()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> gender = context.Genders.AsNoTracking()
                    .OrderBy(n => n.Name)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.Name
                        }).ToList();
                var gendertip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select Location ---"
                };
                gender.Insert(0, gendertip);
                return new SelectList(gender, "Value", "Text");
            }
        }
    }
}