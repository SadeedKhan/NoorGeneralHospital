using Microsoft.AspNet.Identity;
using NoorGeneralHospital.Helper.Enum;
using NoorGeneralHospital.Helper.Extention;
using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.OutputDTO;
using NoorGeneralHospital.Models.Sp_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Controllers
{
    [Authorize]
    public class DoctorScheduleController : AdminController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: DoctorSchedule
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            IEnumerable<DoctorSchedule_GetDoctorScheduleDetails> list;
            try
            {
                list = db.Database.SqlQuery<DoctorSchedule_GetDoctorScheduleDetails>("dbo.Sp_GetDoctorScheduleDetails").ToList();
            }
            catch (Exception e)
            {
                list = new List<DoctorSchedule_GetDoctorScheduleDetails>();
            }
            return PartialView("Details", list);
        }


        [HttpPost]
        public ActionResult SaveDoctorSchedule(DoctorScheduleInput docseh)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = User.Identity.GetUserId();
            var res = 0;
            if(!ExtentionMethods.IsNullOrEmpty(docseh.AvailableDays))
            {
                docseh.AvailableDay = String.Join(",", docseh.AvailableDays);
            }    
            try
            {
                DoctorSchedule doctorseh = db.DoctorSchedules.Where(x => x.Id == docseh.Id).FirstOrDefault();
                if (doctorseh != null)
                {
                    var local = db.Set<Doctor>().Local.FirstOrDefault(f => f.Id == doctorseh.Id);
                    if (local != null)
                    {
                        db.Entry(local).State = EntityState.Detached;
                    }
                    doctorseh.DoctorId = docseh.DoctorId;
                    doctorseh.AvailableDay = docseh.AvailableDay;
                    doctorseh.StartTime = docseh.StartTime.ToString();
                    doctorseh.EndTime = docseh.StartTime.ToString();
                    doctorseh.Description = docseh.Description;
                    doctorseh.IsActive = docseh.IsActive;
                    doctorseh.CreatedById = doctorseh.CreatedById;
                    doctorseh.CreatedOn = doctorseh.CreatedOn;
                    doctorseh.UpdatedOn = DateTime.Now;
                    doctorseh.UpdatedById = userId;
                    db.Entry(doctorseh).State = EntityState.Modified;
                    res = db.SaveChanges();
                    _result.Message = "Record Updated Successfully!";
                    _result.Code = "1";
                }
                else
                {
                    doctorseh = new DoctorSchedule();
                    doctorseh.DoctorId = docseh.DoctorId;
                    doctorseh.AvailableDay = docseh.AvailableDay;
                    doctorseh.StartTime = docseh.StartTime.ToString(); 
                    doctorseh.EndTime = docseh.EndTime.ToString();
                    doctorseh.Description = docseh.Description;
                    doctorseh.IsActive = docseh.IsActive;
                    doctorseh.CreatedOn = DateTime.Now;
                    doctorseh.CreatedById = userId;
                    db.DoctorSchedules.Add(doctorseh);
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


        [HttpPost]
        public ActionResult AddEditDoctorSchedule(int? id)
        {
            DoctorScheduleInput doctorseh;
            if (id > 0)
            {
                DoctorSchedule docseh = db.DoctorSchedules.Find(id);
                doctorseh = new DoctorScheduleInput();
                doctorseh.DoctorId = docseh.DoctorId;
                var arr=docseh.AvailableDay.Split(',').Select(Int32.Parse).ToArray();
                doctorseh.AvailableDays = arr;
                doctorseh.StartTime = docseh.StartTime;
                doctorseh.EndTime = docseh.EndTime;
                doctorseh.Description = docseh.Description;
                doctorseh.IsActive = docseh.IsActive;
                doctorseh.Id = docseh.Id;
            }
            else
            {
                doctorseh = new DoctorScheduleInput();
            }
            return PartialView("AddEditDoctorSchedule", doctorseh);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = User.Identity.GetUserId();
            try
            {
                if (id > 0)
                {
                    DoctorSchedule doctorsch = db.DoctorSchedules.Find(id);
                    doctorsch.UpdatedOn = DateTime.Now;
                    doctorsch.UpdatedById = userId;
                    doctorsch.IsActive = false;
                    db.Entry(doctorsch).State = EntityState.Modified;
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