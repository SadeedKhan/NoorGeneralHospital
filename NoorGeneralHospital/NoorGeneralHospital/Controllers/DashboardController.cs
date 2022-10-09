using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.Sp_Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Controllers
{
    [Authorize]
    public class DashboardController : AdminController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: Dashboard
        public ActionResult Index()
        
        {
            Dashboard_DashboardStats obj;
            try
            {
                obj = db.Database.SqlQuery<Dashboard_DashboardStats>("dbo.Sp_Dashboard_DashboardStats").SingleOrDefault();
            }
            catch (Exception e)
            {
                obj = new Dashboard_DashboardStats();
            }
            return View(obj);
        }

        //Get Doctors
        [HttpGet]
        public ActionResult DashboardDoctorsDetails()
        {
            IEnumerable<Doctor_GetDoctorDetails> list;
            try
            {
                var context = db.Database.ExecuteSqlCommand(
                        @"EXEC dbo.Doctor_GetDoctorDetails @Id",
                        new SqlParameter("Id", int.Parse("0")));
                list = db.Database.SqlQuery<Doctor_GetDoctorDetails>("Doctor_GetDoctorDetails @Id", new SqlParameter("@Id", (object)0)).ToList();
            }
            catch (Exception e)
            {
                list = new List<Doctor_GetDoctorDetails>();
            }
            return PartialView("DashboardDoctorsDetails", list);
        }

        //Get Appointment
        [HttpGet]
        public ActionResult DashboardAppointmentDetails()
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
            return PartialView("DashboardAppointmentDetails", list);
        }
    }
}