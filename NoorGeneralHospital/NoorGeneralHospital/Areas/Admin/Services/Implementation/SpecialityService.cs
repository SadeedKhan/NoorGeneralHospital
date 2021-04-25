using Microsoft.AspNet.Identity;
using NoorGeneralHospital.Areas.Admin.Models.Auth;
using NoorGeneralHospital.Areas.Admin.Models.Entities;
using NoorGeneralHospital.Areas.Admin.Models.OutputDTO;
using NoorGeneralHospital.Areas.Admin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NoorGeneralHospital.Areas.Admin.Services.Implementation
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;

        public SpecialityService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this._db = db;
        }

        public List<Speciality> GetSpecialities()
        {
            List<Speciality> data;
            try
            {
                 data = _db.Speciality.ToList();
            }
            catch (Exception e)
            {
                data = null;
            }
            return data;
        }

        public Speciality GetSpecialityById(Guid? specialityId)
        {
            var data = _db.Speciality.Where(x => x.Id == specialityId).SingleOrDefault();
            return data;
        }

        public async Task<GeneralResponse> AddOrUpdateEmployee(Speciality speciality)
        {
            GeneralResponse _result = new GeneralResponse();
            try
            {
                var user = await userManager.FindByIdAsync(HttpContext.Current.User.Identity.GetUserId());
                var Exist = _db.Speciality.Where(x => x.Id == speciality.Id).FirstOrDefault();
                if (Exist != null)
                {
                    speciality.UpdatedOn = DateTime.Now;
                    speciality.UpdatedById = user.ToString();
                    _db.Entry(speciality).State = EntityState.Modified;
                    var res = await _db.SaveChangesAsync();
                    _result.Message = "Record Updated Successfully!";
                    _result.Code = "1";
                }
                else
                {
                    speciality.Id = Guid.NewGuid();
                    speciality.CreatedById = user.ToString();
                    _db.Entry(speciality).State = EntityState.Added;
                    var res = await _db.SaveChangesAsync();
                    _result.Message = "Record Created Successfully!";
                    _result.Code = "1";
                }
            }
            catch (Exception e)
            {
                _result.Message = "An Internal Error!";
                _result.Code = "0";
            }
            return _result;
        }

        public GeneralResponse DeleteSpeciality(Guid specialityId)
        {

            GeneralResponse _result = new GeneralResponse();
            try
            {
                var record = _db.Speciality
              .FirstOrDefault(e => e.Id == specialityId);
                if (record != null)
                {
                    _db.Speciality.Remove(record);
                    _db.SaveChangesAsync();
                    _result.Message = "Speciality Removed Successfully!";
                    _result.Code = "1";
                }
            }
            catch (Exception e)
            {
                _result.Message = "Failed To Delete Speciality!";
                _result.Code = "0";

            }
            return _result;
        }
    }
}