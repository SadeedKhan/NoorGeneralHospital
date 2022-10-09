using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NoorGeneralHospital.Helper;
using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.OutputDTO;
using NoorGeneralHospital.Models.Sp_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace NoorGeneralHospital.Controllers
{
    public class AppointmentController : AdminController
    {
        private static readonly ApplicationDbContext db = new ApplicationDbContext();
        private static UserManager<ApplicationUser> _UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakeAnAppointment()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            IEnumerable<Appointment_GetAppointmentDetails> list;
            try
            {
                list = db.Database.SqlQuery<Appointment_GetAppointmentDetails>("dbo.Sp_GetAppointmentDashboardDetail").ToList();
            }
            catch (Exception e)
            {
                list = new List<Appointment_GetAppointmentDetails>();
            }
            return PartialView("Details", list);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveAppointment(AppointmentInput ap)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = User.Identity.GetUserId();
            ap.AppointmentTime = ap.AppointmentTime.Replace(" ", string.Empty);
            try
            {
                DateTime dateTime;
                dateTime = DateTimeFormats.ConvertStrings(ap.AppointmentDate, ap.AppointmentTime);
                db.Appointments.Add(new Appointment
                {
                    AppointmentDate = dateTime,
                    UserId = userId,
                    StatusId=1,
                    SpecialityId = ap.SpecialityId,
                    DoctorId = ap.DoctorId,
                    CreatedById=userId,
                    CreatedOn = DateTime.Now,
                });
                db.SaveChanges();
                _result.Message = "Record Created Successfully!";
                _result.Code = "1";
            }
            catch (Exception ex)
            {
                _result.Message = ex.Message;
                _result.Code = "0";
            }
            return Json(_result);




            //try
            //{
            //    Appointment appointment = db.Appointments.Where(x => x.Id == ap.Id).FirstOrDefault();
            //    if (appointment != null)
            //    {
            //        var local = db.Set<Appointment>().Local.FirstOrDefault(f => f.Id == appointment.Id);
            //        if (local != null)
            //        {
            //            db.Entry(local).State = EntityState.Detached;
            //        }
            //        Appointment appoint = new Appointment();
            //        appoint.Id = ap.Id;
            //        appoint.SpecialityId = ap.SpecialityId;
            //        appoint.DoctorId = ap.DoctorId;
            //        appoint.AppointmentDate = ap.AppointmentDate;
            //        appoint.Description = ap.Description;
            //        appoint.CreatedById = appointment.CreatedById;
            //        appoint.CreatedOn = appointment.CreatedOn;
            //        appoint.UpdatedOn = DateTime.Now;
            //        appoint.UpdatedById = userId;
            //        db.Entry(appoint).State = EntityState.Modified;
            //        res = db.SaveChanges();
            //        _result.Message = "Record Updated Successfully!";
            //        _result.Code = "1";
            //    }
            //    else
            //    {
            //        appointment = new Appointment();
            //        appointment.SpecialityId = ap.SpecialityId;
            //        appointment.DoctorId = ap.DoctorId;
            //        appointment.AppointmentDate = ap.AppointmentDate;
            //        appointment.Description = ap.Description;
            //        appointment.IsActive = true;
            //        appointment.StatusId = 1;
            //        appointment.CreatedOn = DateTime.Now;
            //        appointment.CreatedById = userId;
            //        db.Appointments.Add(appointment);
            //        res = db.SaveChanges();
            //        _result.Message = "Record Created Successfully!";
            //        _result.Code = "1";
            //        if(res>0)
            //        {
            //            //Add Appointment For Activities
            //            Activities activities = new Activities();
            //            activities.AppointmentDate = ap.AppointmentDate; 
            //            db.activities.Add(activities);
            //            var result = db.SaveChanges();
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    _result.Message = "An Internal Error!";
            //    _result.Code = "0";
            //}
        }


        [HttpPost]
        public ActionResult AddEditAppointment()
        {
            return PartialView("AddEditAppointment", new AppointmentInput());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            GeneralResponse _result = new GeneralResponse();
            try
            {
                if (id > 0)
                {
                    Appointment ap = db.Appointments.Find(id);
                    db.Appointments.Remove(ap);
                    db.SaveChanges();
                    _result.Message = "Appointment Removed Successfully!";
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

        //Update Doctor Dropdown
        [HttpPost]
        public JsonResult PopulateDoctorsListBySpecialityId(int SpecialityId)
        {
            if(SpecialityId>0)
            {
                var lst = db.Doctors.Where(x => x.IsActive == true && x.SpecialityId == SpecialityId).AsNoTracking()
                                   .OrderBy(n => n.UserName)
                                       .Select(n =>
                                       new SelectListItem
                                       {
                                           Value = n.Id.ToString(),
                                           Text = n.UserName
                                       }).ToList();
                return Json(lst);
            }
                else
            {
                return Json(null);
            }
        }

        [HttpPost]
        public ActionResult AcceptAppointment(int id)
        {
            string userId = User.Identity.GetUserId();
            GeneralResponse _result = new GeneralResponse();
            try
            {
                if (id > 0)
                {
                    Appointment ap = db.Appointments.Find(id);
                    ap.UpdatedOn = DateTime.Now;
                    ap.UpdatedById = userId;
                    ap.StatusId = 2;
                    db.Entry(ap).State = EntityState.Modified;
                    db.SaveChanges();
                    _result.Message = "Appointment Accepted Successfully!";
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
        public ActionResult AttendAppointment(int id)
        {
            string userId = User.Identity.GetUserId();
            GeneralResponse _result = new GeneralResponse();
            try
            {
                if (id > 0)
                {
                    Appointment ap = db.Appointments.Find(id);
                    ap.UpdatedOn = DateTime.Now;
                    ap.UpdatedById = userId;
                    ap.StatusId = 3;
                    db.Entry(ap).State = EntityState.Modified;
                    db.SaveChanges();
                    _result.Message = "Appointment Attend Successfully!";
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
        public ActionResult RejectAppointmentPartialView(int Id)
        {
            RejectionInput obj = new RejectionInput();
            try
            {
                Appointment ap = db.Appointments.Find(Id);
                obj.Id = ap.Id;
            }
            catch (Exception e)
            {
                obj = new RejectionInput();
            }
            return PartialView("RejectAppointment",obj);
        }

        [HttpPost]
        public ActionResult RejectAppointment(RejectionInput ri)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = User.Identity.GetUserId();
            try
            {
                if (ri.Id > 0)
                {
                    Appointment ap = db.Appointments.Find(ri.Id);
                    ap.UpdatedOn = DateTime.Now;
                    ap.UpdatedById = userId;
                    ap.IsActive = false;
                    ap.StatusId = 4;
                    db.Entry(ap).State = EntityState.Modified;
                    db.SaveChanges();
                    _result.Message = "Appointment Reject Successfully!";
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

      
    }
}