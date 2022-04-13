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
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Report
        public ActionResult Index()
        {
            return View(new AppointmentReportInput());
        }

        [HttpPost]
        public ActionResult Details(AppointmentReportInput input)
        {
           
            IEnumerable<Appointment_GetAppointmentDetails> list;
            try
            {
                //var FromDate = "";
                //var ToDate = "";
                //if (!string.IsNullOrEmpty(input.FromDate) && !string.IsNullOrEmpty(input.ToDate))
                //{
                //    FromDate = Convert.ToDateTime(input.FromDate).ToShortDateString();
                //    ToDate = Convert.ToDateTime(input.ToDate).ToShortDateString();
                //}
                SqlParameter _StatusId = new SqlParameter("@StatusId", input.StatusId == null ? DBNull.Value : (object)input.StatusId);
                SqlParameter _FromDate = new SqlParameter("@FromDate", input.FromDate == null ? DBNull.Value : (object)input.FromDate);
                SqlParameter _ToDate = new SqlParameter("@ToDate", input.ToDate == null ? DBNull.Value : (object)input.ToDate);

               // list = _db.Database.SqlQuery<Appointment_GetAppointmentDetails>("dbo.Appointment_GetAppointmentReportDetails " +
               //"@StatusId,@FromDate,@ToDate",
               //_StatusId, _FromDate, _ToDate).ToList();

               list= _db.Database.SqlQuery<Appointment_GetAppointmentDetails>("Sp_GetAppointmentReportDetails @StatusId, @FromDate, @ToDate",
                   _StatusId, _FromDate, _ToDate).ToList();
                //list = db.Database.SqlQuery<Appointment_GetAppointmentDetails>("dbo.Appointment_GetAppointmentReportDetails").ToList();
            }
            catch (Exception e)
            {
                list = new List<Appointment_GetAppointmentDetails>();
            }
            return PartialView("Details", list);
        }
    }
}