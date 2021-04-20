using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Areas.Admin.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Admin/ManageAppointment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _AddAppointment()
        {
            return View();
        }
    }
}