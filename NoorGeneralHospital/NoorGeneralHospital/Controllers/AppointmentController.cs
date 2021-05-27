using Microsoft.AspNet.Identity;
using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.OutputDTO;
using NoorGeneralHospital.Models.Sp_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            IEnumerable<Appointment_GetAppointmentDetails> list;
            try
            {
                list = db.Database.SqlQuery<Appointment_GetAppointmentDetails>("dbo.Appointment_GetAppointmentDetails").ToList();
            }
            catch (Exception e)
            {
                list = new List<Appointment_GetAppointmentDetails>();
            }
            return PartialView("Details", list);
        }

        [HttpPost]
        public ActionResult SaveAppointment(AppointmentInput ap)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = User.Identity.GetUserId();
            var res = 0;
            try
            {
                Appointment appointment = db.Appointments.Where(x => x.Id == ap.Id).FirstOrDefault();
                if (appointment != null)
                {
                    var local = db.Set<Appointment>().Local.FirstOrDefault(f => f.Id == appointment.Id);
                    if (local != null)
                    {
                        db.Entry(local).State = EntityState.Detached;
                    }
                    Appointment appoint = new Appointment();
                    appoint.Id = ap.Id;
                    appoint.PatientAge = ap.PatientAge;
                    appoint.PatientName = ap.PatientName;
                    appoint.PatientEmail = ap.PatientEmail;
                    appoint.PatientPhone = ap.PatientPhone;
                    appoint.SpecialityId = ap.SpecialityId;
                    appoint.DoctorId = ap.DoctorId;
                    appoint.AppointmentDate = ap.AppointmentDate;
                    appoint.Description = ap.Description;
                    appoint.UpdatedOn = DateTime.Now;
                    appoint.UpdatedById = userId;
                    db.Entry(appoint).State = EntityState.Modified;
                    res = db.SaveChanges();
                    _result.Message = "Record Updated Successfully!";
                    _result.Code = "1";
                }
                else
                {
                    appointment = new Appointment();
                    appointment.PatientName = ap.PatientName;
                    appointment.PatientAge = ap.PatientAge;
                    appointment.PatientEmail = ap.PatientEmail;
                    appointment.PatientPhone = ap.PatientPhone;
                    appointment.SpecialityId = ap.SpecialityId;
                    appointment.DoctorId = ap.DoctorId;
                    appointment.AppointmentDate = ap.AppointmentDate;
                    appointment.Description = ap.Description;
                    appointment.IsActive = true;
                    appointment.StatusId = 1;
                    appointment.CreatedOn = DateTime.Now;
                    appointment.CreatedById = userId;
                    db.Appointments.Add(appointment);
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
        public ActionResult AddEditAppointment(int? id)
        {
            AppointmentInput api;
            if (id > 0)
            {
                Appointment ap = db.Appointments.Find(id);
                api = new AppointmentInput();
                api.PatientName = ap.PatientName;
                api.PatientAge = ap.PatientAge;
                api.PatientEmail = ap.PatientEmail;
                api.PatientPhone = ap.PatientPhone;
                api.SpecialityId = ap.SpecialityId;
                api.DoctorId = ap.DoctorId;
                api.AppointmentDate = ap.AppointmentDate;
                api.Description = ap.Description;
                api.Id = ap.Id;
            }
            else
            {
                api = new AppointmentInput();
            }
            return PartialView("AddEditAppointment", api);
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
                    Appointment ap = db.Appointments.Find(id);
                    ap.UpdatedOn = DateTime.Now;
                    ap.UpdatedById = userId;
                    ap.IsActive = false;
                    ap.StatusId = 4;
                    db.Entry(ap).State = EntityState.Modified;
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
        public JsonResult PopulateDoctorsList(int? SpecialityId)
        {
            if (SpecialityId > 0)
            {
                var lst = db.Doctors.Where(x => x.Id == SpecialityId && x.IsActive == true).AsNoTracking()
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