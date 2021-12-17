using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    //public class MonthlySummaryModel
    //{
    //    public class SummaryEntry {
    //        public string name;
    //        public string code;
    //        public int entries;
    //        public int time;
    //        public int accepted_time;
    //    }
    //    public MonthlySummaryModel(string user) : this(user, DateTime.Now) {}
    //    public MonthlySummaryModel(string user, DateTime date) {
    //        var report = Entities.Report.load(user, date);
    //        var activities = Entities.Activities.load();
    //        foreach (var activity in activities.activities) {
    //            var es = report.entries.FindAll(e => e.code.Equals(activity.code));
    //            var aes = report.acceptedEntries.FindAll(e => e.code.Equals(activity.code));
    //            var time = es.Sum(e => e.time);
    //            var atime = aes.Sum(e => e.time);
    //            var ecount = es.ToList().Count;
    //            if (ecount > 0)
    //            {
    //                this.entries.Add(new SummaryEntry
    //                {
    //                    name = activity.name,
    //                    code = activity.code,
    //                    entries = ecount,
    //                    time = time,
    //                    accepted_time = atime,
    //                });
    //            }
    //        }
    //        this.frozen = report.frozen;
    //        this.date = date;
    //    }
    //    public List<SummaryEntry> entries = new();
    //    public bool frozen;
    //    public DateTime date;
    //}
}
