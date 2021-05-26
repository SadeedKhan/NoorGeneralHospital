using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoorGeneralHospital.Models.InputDTO
{
    public class AppointmentReportInput
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string StatusId { get; set; }

        public IEnumerable<SelectListItem> GetStatus()
        {
            using (var context = new ApplicationDbContext())
            {
                List<SelectListItem> status = context.Statuses.AsNoTracking()
                    .OrderBy(n => n.Id)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.StatusName
                        }).ToList();
                var Statustip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Status ---"
                };
                status.Insert(0, Statustip);
                return new SelectList(status, "Value", "Text");
            }
        }
    }
}