using NoorGeneralHospital.Areas.Admin.Models.Entities;
using NoorGeneralHospital.Areas.Admin.Models.OutputDTO;
using NoorGeneralHospital.Areas.Admin.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Areas.Admin.Controllers
{
    public class SpecialityController : Controller
    {
        public SpecialityController() { }

        private readonly ISpecialityService _specialityService;
        public SpecialityController(ISpecialityService speciality)
        {
            this._specialityService = speciality;
        }

        // GET: Admin/Speciality
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _SpecialityDetail()
        {
            List<Speciality> list = new List<Speciality>();
            GeneralResponse _result = new GeneralResponse();
            try
            {
                list = _specialityService.GetSpecialities();
            }
            catch (Exception e)
            {
                _result.Message = "Failed to fetch records!";
                _result.Code = "0";
            }
            return PartialView(list);
        }

        [HttpPost]
        public ActionResult SaveUpdateSpeciality(Speciality speciality)
         {
            GeneralResponse _result = new GeneralResponse();
            try
            {
                    if (speciality == null)
                    {
                        _result.Message = "Speciality data is not Valid!";
                        _result.Code = "0";
                    }
                    var res = _specialityService.AddOrUpdateEmployee(speciality);
                    return Json(new { Response = res });
            }
            catch (Exception e)
            {
                _result.Message = "Failed to Save/Update Speciality!";
                _result.Code = "0";
            }
            return Json(new { Response = _result });
        }

        [HttpPost]
        public ActionResult _AddEditSpeciality(Guid Id)
         {
            Speciality speciality = _specialityService.GetSpecialityById(Id);
            if (speciality == null)
            {
                speciality= new Speciality();
            }
            return PartialView(speciality);
         }
    }
}