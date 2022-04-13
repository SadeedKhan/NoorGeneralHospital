using Microsoft.AspNet.Identity;
using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.InputDTO;
using NoorGeneralHospital.Models.OutputDTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: Services
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            IEnumerable<Services> list;
            try
            {
                list = db.Services.ToList();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View("Error"); ;
            }
            return PartialView("Details", list);
        }


        [HttpPost]
        public ActionResult SaveServices(ServiceInput serviceInput)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = User.Identity.GetUserId();
            var res = 0;
            try
            {
                Services services = db.Services.Where(x => x.Id == serviceInput.Id).FirstOrDefault();
                if (services != null)
                {
                    var local = db.Set<Services>().Local.FirstOrDefault(f => f.Id == services.Id);
                    if (local != null)
                    {
                        db.Entry(local).State = EntityState.Detached;
                    }
                    Services ser = new Services();
                    ser.Id = serviceInput.Id;
                    ser.ServiceTitle = serviceInput.ServiceTitle;
                    ser.Description = serviceInput.Description;
                    ser.IsActive = serviceInput.IsActive;
                    ser.CreatedOn = services.CreatedOn;
                    ser.UpdatedById = services.CreatedById;
                    ser.UpdatedOn = DateTime.Now;
                    ser.UpdatedById = userId;
                    if (serviceInput.file != null)
                    {
                        ser.ImagePath = WriteFile(serviceInput.file);
                    }
                    else
                    {
                        ser.ImagePath = services.ImagePath;
                    }
                    db.Entry(ser).State = EntityState.Modified;
                    res = db.SaveChanges();
                    _result.Message = "Record Updated Successfully!";
                    _result.Code = "1";
                }
                else
                {
                    services = new Services();
                    services.ServiceTitle = serviceInput.ServiceTitle;
                    services.Description = serviceInput.Description;
                    services.IsActive = serviceInput.IsActive;
                    services.CreatedOn = DateTime.Now;
                    services.CreatedById = userId;
                    if (serviceInput.file != null)
                    {
                        services.ImagePath = WriteFile(serviceInput.file);
                    }
                    db.Services.Add(services);
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

        private string WriteFile(HttpPostedFileWrapper file)
        {
            bool WriteFileWithRoot = true;
            string fileName = null;
            string pathwithfilename;
            try
            {
                if (WriteFileWithRoot == true)
                {
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.
                    var pathBuilt = Path.Combine(Server.MapPath("~/Uploads/Services/"));
                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }
                    var path = Path.Combine(Server.MapPath("~/Uploads/Services/"), fileName);
                    file.SaveAs(path);
                }
                pathwithfilename = "Uploads/Services/" + fileName;
            }
            catch (Exception e)
            {
                pathwithfilename = "";
            }
            return pathwithfilename;
        }

        [HttpPost]
        public ActionResult AddEditServices(int? id)
        {
            ServiceInput service;
            if (id > 0)
            {
                Services ser = db.Services.Find(id);
                service = new ServiceInput();
                service.ServiceTitle = ser.ServiceTitle;
                service.Description = ser.Description;
                service.IsActive = ser.IsActive;
                service.Id = ser.Id;
            }
            else
            {
                service = new ServiceInput();
            }
            return PartialView("AddEditServices", service);
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
                    Services ser = db.Services.Find(id);
                    ser.UpdatedOn = DateTime.Now;
                    ser.UpdatedById = userId;
                    ser.IsActive = false;
                    db.Entry(ser).State = EntityState.Modified;
                    db.SaveChanges();
                    _result.Message = "Services Removed Successfully!";
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