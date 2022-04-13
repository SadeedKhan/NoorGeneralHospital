using Microsoft.AspNet.Identity;
using NoorGeneralHospital.Models;
using NoorGeneralHospital.Models.OutputDTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            List<Location> list = new List<Location>();
            try
            {
                list = db.Locations.ToList();
            }
            catch (Exception e)
            {
                list = new List<Location>();
            }
            return PartialView("Details", list);
        }

        [HttpPost]
        public ActionResult SaveLocation(Location location)
        {
            GeneralResponse _result = new GeneralResponse();
            string userId = User.Identity.GetUserId();
            int Exist;
            try
            {
                Location loc = db.Locations.Where(x => x.Id == location.Id).FirstOrDefault();
                if (loc != null)
                {
                    Exist = db.Locations.Where(a => a.LocationName == location.LocationName && a.Id != location.Id && a.IsActive == true).Count();
                    if (Exist > 0)
                    {
                        _result.Message = "Record Already Exist With Same Name!";
                        _result.Code = "2";
                    }
                    else
                    {
                        var local = db.Set<Location>().Local.FirstOrDefault(f => f.Id == location.Id);
                        if (local != null)
                        {
                            db.Entry(local).State = EntityState.Detached;
                        }
                        location.NormalizeLocationName = location.LocationName.ToUpper();
                        location.UpdatedOn = DateTime.Now;
                        location.UpdatedById = userId;
                        location.CreatedOn = loc.CreatedOn;
                        location.CreatedById = loc.CreatedById;
                        db.Entry(location).State = EntityState.Modified;
                        db.SaveChanges();
                        _result.Message = "Record Updated Successfully!";
                        _result.Code = "1";
                    }
                }
                else
                {
                    Exist = db.Locations.Where(a => a.LocationName == location.LocationName).Count();
                    if (Exist > 0)
                    {
                        _result.Message = "Record Already Exist With Same Name!";
                        _result.Code = "2";
                    }
                    else
                    {
                        location.NormalizeLocationName = location.LocationName.ToUpper();
                        location.CreatedOn = DateTime.Now;
                        location.CreatedById = userId;
                        db.Locations.Add(location);
                        db.SaveChanges();
                        _result.Message = "Record Created Successfully!";
                        _result.Code = "1";
                    }
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
        public ActionResult AddEditlocation(int? id)
        {
            Location location;
            if (id > 0)
            {
                location = db.Locations.Find(id);
            }
            else
            {
                location = new Location();
            }
            return PartialView("AddEditLocation", location);
        }

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    GeneralResponse _result = new GeneralResponse();
        //    string userId = User.Identity.GetUserId();
        //    try
        //    {
        //        if (id > 0)
        //        {
        //            Location location = db.Locations.Find(id);
        //            location.UpdatedOn = DateTime.Now;
        //            location.UpdatedById = userId;
        //            location.IsActive = false;
        //            db.Entry(location).State = EntityState.Modified;
        //            db.SaveChanges();
        //            _result.Message = "Location Removed Successfully!";
        //            _result.Code = "1";
        //        }
        //        else
        //        {
        //            _result.Message = "InValid Id!";
        //            _result.Code = "0";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _result.Message = "An Internal Error!";
        //        _result.Code = "0";
        //    }
        //    return Json(_result);
        //}

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