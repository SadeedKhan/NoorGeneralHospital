using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.Sp_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
                list = db.Sp_Doctor_GetDoctorDetails.SqlQuery($"Sp_GetDoctorDetails @Id", new SqlParameter("@Id", (object)0)).ToList();
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
                doc = db.Database.SqlQuery<Doctor_GetDoctorDetailsByID>("Sp_GetDoctorDetailsByID @Id", new SqlParameter("@Id", (object)Id)).FirstOrDefault();
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

        [HttpGet]
        public ActionResult MakeAnAppointment()
        {
            return PartialView("MakeAnAppointment", new AppointmentInput());
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
            return Json(lst,JsonRequestBehavior.AllowGet);
        }
    }
}