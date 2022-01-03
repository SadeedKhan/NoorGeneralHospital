using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.Sp_Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
                list = db.Database.SqlQuery<Doctor_GetDoctorDetails>("Doctor_GetDoctorDetails @Id", new SqlParameter("@Id", (object)0)).ToList();
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
            Doctor_GetDoctorDetails doc;
            try
            {
                doc = db.Database.SqlQuery<Doctor_GetDoctorDetails>("Doctor_GetDoctorDetails @Id", new SqlParameter("@Id", (object)0)).ToList();
            }
            catch (Exception e)
            {
                doc = new Doctor_GetDoctorDetails();
            }
            //return PartialView("Details", list);
            return View(doc);
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult About()
        {

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult MakeAnAppointment()
        {
            return PartialView("MakeAnAppointment", new AppointmentInput());
        }
    }
}