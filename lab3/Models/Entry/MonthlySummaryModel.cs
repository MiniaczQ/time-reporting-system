using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Datetime;
using lab1.debug;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lab1.Models
{
    public class MonthlySummaryModel
    {
        public MonthlySummaryModel(DateTime Date, string UserName)
        {
            var ReportMonth = Date.Dayless();
            using (var db = new LabContext())
            {
                var r = db.Reports.Where(r => r.ReportMonth == ReportMonth && r.UserName == UserName);
                this.Report = r.FirstOrDefault();
                if (this.Report == null)
                {
                    this.Report = new Report { ReportMonth = ReportMonth };
                    return;
                }

                var entries_by_activities = r
                    .Include(r => r.Entries)
                    .SelectMany(r => r.Entries)
                    .Include(e => e.Activity)
                    .AsEnumerable()
                    .GroupBy(e => e.Activity);
                var aes = db.AcceptedEntries.Where(ae => ae.ReportMonth == ReportMonth && ae.UserName == UserName);
                this.Triplets = entries_by_activities
                    .GroupJoin(aes,
                    ea => ea.Key.ActivityCode,
                    ae => ae.Activity.ActivityCode,
                    (ea, ae) => new Triplet { Activity = ea.Key, AcceptedEntry = ae.FirstOrDefault(), Entries = ea.ToList() }).ToList();
            }
        }

        public class Triplet
        {
            public Activity Activity;
            public AcceptedEntry AcceptedEntry;
            public List<Entry> Entries = new();
        }

        public Report Report { get; set; }
        public List<Triplet> Triplets { get; set; } = new();
    }
}
