using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.OutputDTO;
using NoorGeneralHospital.Models.Sp_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            IEnumerable<Doctor_GetDoctorDetails> list;
            try
            {
                var context = db.Database.ExecuteSqlCommand(
                        @"EXEC dbo.Doctor_GetDoctorDetails @Id",
                        new SqlParameter("Id", int.Parse("0")));
                list  = db.Database.SqlQuery<Doctor_GetDoctorDetails>("Doctor_GetDoctorDetails @Id", new SqlParameter("@Id",(object)0)).ToList();

            }
            catch (Exception e)
            {
                list = new List<Doctor_GetDoctorDetails>();
            }
            return PartialView("Details", list);
        }


        [HttpPost]
        public ActionResult SaveDoctor(DoctorInput doc)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = "1";
            var res=0;
            try
            {
                    Doctor doctor = db.Doctors.Where(x => x.Id == doc.Id).FirstOrDefault();
                    if (doctor != null)
                    {
                        var local = db.Set<Doctor>().Local.FirstOrDefault(f => f.Id == doctor.Id);
                        if (local != null)
                        {
                            db.Entry(local).State = EntityState.Detached;
                        }
                    Doctor doct = new Doctor();
                    doct.Id = doc.Id;
                    doct.UserName = doc.UserName;
                    doct.Email = doc.Email;
                    doct.Phone = doc.Phone;
                    doct.SpecialityId = doc.SpecialityId;
                    doct.GenderId = doc.GenderId;
                    doct.LocationId = doc.LocationId;
                    doct.Services = doc.Services;
                    doct.Education = doc.Education;
                    doct.Experience = doc.Experience;
                    doct.DOB = doc.DOB;
                    doct.Address = doc.Address;
                    doct.ShortBioGraphy = doc.ShortBioGraphy;
                    doct.IsActive = doc.IsActive;
                    doct.UpdatedOn = DateTime.Now;
                    doct.UpdatedById = userId;
                    if (doc.file != null)
                    {
                        doct.ImagePath = WriteFile(doc.file);
                    }
                    else
                    {
                        doct.ImagePath = doctor.ImagePath;
                    }
                    db.Entry(doct).State = EntityState.Modified;
                       res=db.SaveChanges();
                        _result.Message = "Record Updated Successfully!";
                        _result.Code = "1";
                    }
                    else
                    {
                    doctor = new Doctor();
                    doctor.UserName = doc.UserName;
                    doctor.Email = doc.Email;
                    doctor.Phone = doc.Phone;
                    doctor.SpecialityId = doc.SpecialityId;
                    doctor.GenderId = doc.GenderId;
                    doctor.LocationId = doc.LocationId;
                    doctor.Services = doc.Services;
                    doctor.Education = doc.Education;
                    doctor.Experience = doc.Experience;
                    doctor.DOB = doc.DOB;
                    doctor.Address = doc.Address;
                    doctor.ShortBioGraphy = doc.ShortBioGraphy;
                    doctor.IsActive = doc.IsActive;
                    doctor.CreatedOn = DateTime.Now;
                    doctor.CreatedById = userId;
                    if (doc.file != null)
                    {
                       doctor.ImagePath= WriteFile(doc.file);
                    }
                    db.Doctors.Add(doctor);
                   res = db.SaveChanges();
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

        private string WriteFile(HttpPostedFileWrapper file)
        {
            GeneralResponse _result = new GeneralResponse();
            bool WriteFileWithRoot = true;
            string fileName = null;
            string pathwithfilename;
            try
            {
                if (WriteFileWithRoot == true)
                {
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.
                    var pathBuilt =Path.Combine(Server.MapPath("~/Uploads/Doctors/"));
                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }
                    var path = Path.Combine(Server.MapPath("~/Uploads/Doctors/"), fileName);
                         file.SaveAs(path);
                }
                pathwithfilename = "Uploads/Doctors/" + fileName; 
            }
            catch (Exception e)
            {
                pathwithfilename = "";
            }
            return pathwithfilename;
        }

        [HttpPost]
        public ActionResult AddEditDoctor(int? id)
        {
            DoctorInput doctor;
            if (id > 0)
            {
                Doctor doc = db.Doctors.Find(id);
                doctor = new DoctorInput();
                doctor.UserName = doc.UserName;
                doctor.Email = doc.Email;
                doctor.Phone = doc.Phone;
                doctor.SpecialityId = doc.SpecialityId;
                doctor.GenderId = doc.GenderId;
                doctor.LocationId = doc.LocationId;
                doctor.Services = doc.Services;
                doctor.Education = doc.Education;
                doctor.Experience = doc.Experience;
                doctor.DOB = doc.DOB;
                doctor.Address = doc.Address;
                doctor.ShortBioGraphy = doc.ShortBioGraphy;
                doctor.IsActive = doc.IsActive;
                doc.Id = doctor.Id;
            }
            else
            {
                doctor = new DoctorInput();
            }
            return PartialView("AddEditDoctor", doctor);
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
                    _result.Message = "Doctor Removed Successfully!";
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

        [HttpPost]
        public ActionResult DoctorProfile(int Id)
        {
            Doctor_GetDoctorDetails Doc;
            try
            {
                Doc = db.Database.SqlQuery<Doctor_GetDoctorDetails>("Doctor_GetDoctorDetails @Id", new SqlParameter("@Id", (object)Id)).SingleOrDefault();
            }
            catch (Exception e)
            {
                Doc = new Doctor_GetDoctorDetails();
            }
            return PartialView("DoctorProfile", Doc);
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