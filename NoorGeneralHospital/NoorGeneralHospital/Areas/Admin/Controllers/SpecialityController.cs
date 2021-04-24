using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Areas.Admin.Controllers
{
    public class SpecialityController : Controller
    {
        // GET: Admin/Speciality
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult _AddSpeciality()
        {
            return View();
        }
    }
}