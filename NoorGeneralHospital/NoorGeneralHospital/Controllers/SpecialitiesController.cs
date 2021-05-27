using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.OutputDTO;

namespace NoorGeneralHospital.Controllers
{
    [Authorize]
    public class SpecialitiesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Specialities
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            List<Speciality> list = new List<Speciality>();
            try
            {
                list = db.Specialities.ToList();
            }
            catch (Exception e)
            {
                list = new List<Speciality>();
            }
            return PartialView("Details", list);
        }


        // POST: Specialities/CreateUpdateSpeciality
        [HttpPost]
        public ActionResult SaveSpeciality(Speciality speciality)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = User.Identity.GetUserId();
            int Exist;
            try
            {
                var Id = db.Specialities.Where(x => x.Id == speciality.Id).FirstOrDefault();
                if (Id != null)
                {
                    Exist = db.Specialities.Where(a => a.NormalizeSpecialityName == speciality.SpecialityName.ToUpper() && a.Id != speciality.Id && a.IsActive == true).Count();
                    if (Exist > 0)
                    {
                        _result.Message = "Record Already Exist With Same Name!";
                        _result.Code = "2";
                    }
                    else
                    {
                        var local = db.Set<Speciality>().Local.FirstOrDefault(f => f.Id == speciality.Id);
                        if (local != null)
                        {
                            db.Entry(local).State = EntityState.Detached;
                        }
                        speciality.NormalizeSpecialityName = speciality.SpecialityName.ToUpper();
                        speciality.UpdatedOn = DateTime.Now;
                        speciality.UpdatedById = userId;
                        db.Entry(speciality).State = EntityState.Modified;
                        db.SaveChanges();
                        _result.Message = "Record Updated Successfully!";
                        _result.Code = "1";
                    }
                }
                else
                {
                    Exist = db.Specialities.Where(a => a.NormalizeSpecialityName == speciality.SpecialityName.ToUpper()).Count();
                    if (Exist > 0)
                    {
                        _result.Message = "Record Already Exist With Same Name!";
                        _result.Code = "2";
                    }
                    else
                    {
                        speciality.NormalizeSpecialityName = speciality.SpecialityName.ToUpper();
                        speciality.CreatedOn = DateTime.Now;
                        speciality.CreatedById = userId;
                        db.Specialities.Add(speciality);
                        db.SaveChanges();
                        _result.Message = "Record Created Successfully!";
                        _result.Code = "1";
                    }
                }
            }
            catch (Exception e)
            {
                _result.Message = "An Internal Error!";
                _result.Code = "0";
            }
            return Json(_result);
        }

        [HttpPost]
        public ActionResult AddEditSpeciality(int? id)
        {
            Speciality speciality;
            if (id > 0)
            {
                speciality = db.Specialities.Find(id);
            }
            else
            {
                speciality = new Speciality();
            }
            return PartialView("AddEditSpeciality", speciality);
        }

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    GeneralResponse _result = new GeneralResponse();
        //    string userId = User.Identity.GetUserId();
        //    try
        //    {
        //        if (id > 0)
        //        {
        //            Speciality speciality = db.Specialities.Find(id);
        //            speciality.UpdatedOn = DateTime.Now;
        //            speciality.UpdatedById = userId;
        //            speciality.IsActive = false;
        //            db.Entry(speciality).State = EntityState.Modified;
        //            db.SaveChanges();
        //            _result.Message = "Speciality Removed Successfully!";
        //            _result.Code = "1";
        //        }
        //        else
        //        {
        //            _result.Message = "InValid Id!";
        //            _result.Code = "0";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _result.Message = "An Internal Error!";
        //        _result.Code = "0";
        //    }
        //        return Json(_result);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
