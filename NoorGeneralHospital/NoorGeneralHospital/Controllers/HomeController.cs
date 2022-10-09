using Microsoft.AspNet.Identity;
using NoorGeneralHospital.Helper;
using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.Sp_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace NoorGeneralHospital.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Doctors()
        {
            IEnumerable<Doctor_GetDoctorDetails> list;
            try
            {
                list = new List<Doctor_GetDoctorDetails>();
                list = db.Sp_Doctor_GetDoctorDetails.SqlQuery($"Doctor_GetDoctorDetails @Id", new SqlParameter("@Id", (object)0)).ToList();
            }
            catch (Exception e)
            {
                list = new List<Doctor_GetDoctorDetails>();
            }
            //return PartialView("Details", list);
            return View(list);
        }
        public ActionResult SingleDoctor(int Id)

        {
            Doctor_GetDoctorDetailsByID doc;
            try
            {
                doc = db.Database.SqlQuery<Doctor_GetDoctorDetailsByID>("Doctor_GetDoctorDetails @Id", new SqlParameter("@Id", (object)Id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                doc = new Doctor_GetDoctorDetailsByID();
            }
            return View(doc);
        }
        public ActionResult Services()
        {
            List<Services> list;
            try
            {
                list = db.Services.ToList();
            }
            catch (Exception e)
            {
                list = new List<Services>();
            }
            return View(list);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult MakeAnAppointment()
        //{
        //    return PartialView("MakeAnAppointment", new AppointmentInput());
        //}
        [Authorize]
        public ActionResult MakeAnAppointment(int? DoctorId)
        {
            if (DoctorId == null)
               return RedirectToAction("Doctors");
            var result = GetDoctorSpeciality(Convert.ToInt32(DoctorId));
            ViewBag.UserName = result.UserName;
            ViewBag.Speciality = result.Speciality;
            return View(new AppointmentInput()
            {
                DoctorId = (int)DoctorId,
                SpecialityId=result.SpecialityId
            });
        }

        
        private Doctor_GetDoctorDetails GetDoctorSpeciality(int doctorId)
        {
            Doctor_GetDoctorDetails list;
            try
            {
                list = db.Database.SqlQuery<Doctor_GetDoctorDetails>("Doctor_GetDoctorDetails @Id", new SqlParameter("@Id", (object)doctorId)).SingleOrDefault();
            }
            catch (Exception e)
            {
                //ViewBag.ErrorMessage = e.Message;
                //return View("Error");
                list = new Doctor_GetDoctorDetails();
            }
            return  list;
        }

        //Update Doctor Dropdown
        [HttpGet]
        public JsonResult PopulateDoctorsList()
        {
            var lst = db.Doctors.Where(x => x.IsActive == true).AsNoTracking()
                               .OrderBy(n => n.UserName)
                                   .Select(n =>
                                   new SelectListItem
                                   {
                                       Value = n.Id.ToString(),
                                       Text = n.UserName
                                   }).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult MakeAnAppointment(AppointmentInput ap)
        {
            string userId = User.Identity.GetUserId();
            ap.AppointmentDate = DateTimeFormats.convertDate(ap.AppointmentDate);
            ap.AppointmentTime = ap.AppointmentTime.Replace(" ", string.Empty);
            try
            {
                DateTime dateTime;
                dateTime = DateTimeFormats.ConvertStrings(ap.AppointmentDate, ap.AppointmentTime);
                db.Appointments.Add(new Appointment
                {
                    AppointmentDate = dateTime,
                    UserId = userId,
                    StatusId = 1,
                    SpecialityId = ap.SpecialityId,
                    DoctorId = ap.DoctorId,
                    CreatedById = userId,
                    CreatedOn = DateTime.Now,
                });
                db.SaveChanges();

            }
            catch (Exception ex)
            {
               return RedirectToAction("Doctors");
            }
            return RedirectToAction("AppointmentDetails");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AppointmentDetails()
        {
            IEnumerable<Appointment_GetAppointmentDetails> list;
            try
            {
                string userId = User.Identity.GetUserId();
                list = db.Database.SqlQuery<Appointment_GetAppointmentDetails>("dbo.Appointment_GetAppointmentDetails @Id", new SqlParameter("@Id", (object)userId)).ToList();
            }
            catch (Exception e)
            {
                list = new List<Appointment_GetAppointmentDetails>();
            }
            return View(list);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CancelAppointment(int Id)
        {
            try
            {
                Appointment ap = db.Appointments.Find(Id);
                db.Appointments.Remove(ap);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return RedirectToAction("AppointmentDetails");
            }
            return RedirectToAction("AppointmentDetails");
        }
    }
}