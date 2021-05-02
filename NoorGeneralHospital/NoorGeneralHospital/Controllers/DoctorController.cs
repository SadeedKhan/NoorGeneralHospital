using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.OutputDTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Details()
        //{
        //    List<Speciality> list = new List<Doctor>();
        //    try
        //    {
        //        list = db.Specialities.ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        list = new List<Doctor>();
        //    }
        //    return PartialView("Details", list);
        //}


        //[HttpPost]
        //public ActionResult SaveDoctor(DoctorInput doc)
        //{
        //    GeneralResponse _result = new GeneralResponse();
        //    string userId = "1";
        //    var res = 0;
        //    int Exist;
        //    try
        //    {
        //        var Id = db.Specialities.Where(x => x.Id == speciality.Id).FirstOrDefault();
        //        if (Id != null)
        //        {
        //            Exist = db.Specialities.Where(a => a.UserName == speciality.SpecialityName && a.Id != speciality.Id && a.IsActive == true).Count();
        //            if (Exist > 0)
        //            {
        //                _result.Message = "Record Already Exist With Same Name!";
        //                _result.Code = "2";
        //            }
        //            else
        //            {
        //                var local = db.Set<Speciality>().Local.FirstOrDefault(f => f.Id == speciality.Id);
        //                if (local != null)
        //                {
        //                    db.Entry(local).State = EntityState.Detached;
        //                }
        //                speciality.UpdatedOn = DateTime.Now;
        //                speciality.UpdatedById = userId;
        //                db.Entry(speciality).State = EntityState.Modified;
        //                db.SaveChanges();
        //                _result.Message = "Record Updated Successfully!";
        //                _result.Code = "1";
        //            }
        //        }
        //        else
        //        {
        //            Exist = db.Specialities.Where(a => a.SpecialityName == speciality.SpecialityName).Count();
        //            if (Exist > 0)
        //            {
        //                _result.Message = "Record Already Exist With Same Name!";
        //                _result.Code = "2";
        //            }
        //            else
        //            {
        //                speciality.CreatedOn = DateTime.Now;
        //                speciality.CreatedById = userId;
        //                db.Specialities.Add(speciality);
        //                db.SaveChanges();
        //                _result.Message = "Record Created Successfully!";
        //                _result.Code = "1";
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _result.Message = "An Internal Error!";
        //        _result.Code = "0";
        //    }
        //    return Json(_result);
        //}

        //[HttpPost]
        //public ActionResult AddEditSpeciality(int? id)
        //{
        //    Speciality speciality;
        //    if (id > 0)
        //    {
        //        speciality = db.Specialities.Find(id);
        //    }
        //    else
        //    {
        //        speciality = new Speciality();
        //    }
        //    return PartialView("AddEditSpeciality", speciality);
        //}
    }
}