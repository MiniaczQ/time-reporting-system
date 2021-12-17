using System;
using System.Collections.Generic;
using System.Linq;
using lab1.debug;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lab1.Models
{
    public class ActivityInfoModel
    {
        public ActivityInfoModel(string ActivityCode)
        {
            using (var db = new LabContext())
            {
                var a = db.Activities.Where(c => c.ActivityCode == ActivityCode);
                this.Activity = a.FirstOrDefault();

                var entries_by_reports = a
                    .Include(a => a.Entries)
                    .SelectMany(a => a.Entries)
                    .Include(e => e.Report)
                    .ThenInclude(r => r.User)
                    .AsEnumerable()
                    .GroupBy(e => new { UserName = e.UserName, ReportMonth = e.ReportMonth });
                var aes = db.AcceptedEntries.Where(ae => ae.ActivityCode == ActivityCode);
                this.Quartets = entries_by_reports
                    .GroupJoin(aes,
                    ea => ea.Key,
                    ae => new { UserName = ae.UserName, ReportMonth = ae.ReportMonth },
                    (ea, ae) => new Quartet { UserName = ea.Key.UserName, ReportMonth = ea.Key.ReportMonth, AcceptedEntry = ae.FirstOrDefault(), Entries = ea.ToList() }).ToList();
            }
        }

        public class Quartet
        {
            public string UserName { get; set; }
            public DateTime ReportMonth { get; set; }
            public AcceptedEntry AcceptedEntry { get; set; }
            public List<Entry> Entries { get; set; } = new();
        }
        public Activity Activity { get; set; }
        public List<Quartet> Quartets { get; set; } = new();
    }
}
