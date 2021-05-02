using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Models.InputDTO
{
    public class DoctorInput
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int SpecialityId { get; set; }
        public int Gender { get; set; }
        public int LocationId { get; set; }
        public string Services { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string ShortBioGraphy { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        public HttpPostedFileWrapper File { get; set; }

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
    }
}