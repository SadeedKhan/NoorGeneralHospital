using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Areas.Admin.Controllers
{
    public class DoctorsController : Controller
    {
        // GET: Admin/ManageDoctors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AddDoctor()
        {
            return View();
        }
    }
}