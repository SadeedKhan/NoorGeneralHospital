using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.OutputDTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

        [HttpGet]
        public ActionResult Details()
        {
            List<Doctor> list = new List<Doctor>();
            try
            {
                list = db.Doctors.Where(a => a.IsActive == true).ToList();
            }
            catch (Exception e)
            {
                list = new List<Doctor>();
            }
            return PartialView("Details", list);
        }


        [HttpPost]
        public ActionResult SaveDoctor(DoctorInput doc)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = "1";
            try
            {
                if (doc.File != null)
                {
                    bool WriteFileWithRoot = true;
                    string fileName = null;
                    if (WriteFileWithRoot == true)
                    {
                        var extension = "." + doc.File.FileName.Split('.')[doc.File.FileName.Split('.').Length - 1];
                        fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.
                        var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\Doctors");
                        if (!Directory.Exists(pathBuilt))
                        {
                            Directory.CreateDirectory(pathBuilt);
                        }
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\Doctors", fileName);
                        doc.File.SaveAs(path);
                    }
                }

                    Doctor doctor = db.Doctors.Where(x => x.Id == doc.Id).FirstOrDefault();
                    if (doctor != null)
                    {
                        var local = db.Set<Doctor>().Local.FirstOrDefault(f => f.Id == doctor.Id);
                        if (local != null)
                        {
                            db.Entry(local).State = EntityState.Detached;
                        }
                    doctor.UpdatedOn = DateTime.Now;
                    doctor.UpdatedById = userId;
                        db.Entry(doctor).State = EntityState.Modified;
                        db.SaveChanges();
                        _result.Message = "Record Updated Successfully!";
                        _result.Code = "1";
                    }
                    else
                    {
                    doctor.CreatedOn = DateTime.Now;
                    doctor.CreatedById = userId;
                        db.Doctors.Add(doctor);
                        db.SaveChanges();
                        _result.Message = "Record Created Successfully!";
                        _result.Code = "1";
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
        public ActionResult AddEditDoctor(int? id)
        {
            DoctorInput doc;
            if (id > 0)
            {
                Doctor doctor = db.Doctors.Find(id);
                doc = new DoctorInput();
                doc.Id = doctor.Id;
            }
            else
            {
                doc = new DoctorInput();
            }
            return PartialView("AddEditDoctor", doc);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            GeneralResponse _result = new GeneralResponse();
            try
            {
                if (id > 0)
                {
                    Doctor doctor = db.Doctors.Find(id);
                    doctor.UpdatedOn = DateTime.Now;
                    doctor.UpdatedById = "";
                    doctor.IsActive = false;
                    db.Entry(doctor).State = EntityState.Modified;
                    db.SaveChanges();
                    _result.Message = "Speciality Removed Successfully!";
                    _result.Code = "1";
                }
                else
                {
                    _result.Message = "InValid Id!";
                    _result.Code = "0";
                }
            }
            catch (Exception e)
            {
                _result.Message = "An Internal Error!";
                _result.Code = "0";
            }
            return Json(_result);
        }

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