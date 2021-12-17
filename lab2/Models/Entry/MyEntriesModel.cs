using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Datetime;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lab1.Models
{
    public class MyEntriesModel
    {
        public MyEntriesModel() { }
        public MyEntriesModel(string username) : this(username, DateTime.Today) { }
        public MyEntriesModel(string UserName, DateTime DateTime)
        {
            var Date = DateTime.Date;
            var ReportMonth = DateTime.Dayless();
            this.Date = Date;

            using (var db = new LabContext())
            {
                this.Entries = db.Users
                    .Where(u => u.UserName == UserName)
                    .Include(u => u.Reports)
                    .SelectMany(u => u.Reports)
                    .Where(r => r.ReportMonth == ReportMonth)
                    .Include(r => r.Entries)
                    .SelectMany(r => r.Entries)
                    .Where(e => e.Date == Date)
                    .Include(e => e.Activity)
                    .Include(e => e.Report)
                    .OrderBy(e => e.Date)
                    .ToList();
            }

            this.Entries.ForEach(entry => this.TotalTime += entry.Time);
        }

        public DateTime Date { get; set; }
        public List<Entry> Entries { get; set; } = new();
        public int TotalTime { get; set; } = 0;
        public Entry SelectedEntry { get; set; }
    }
}
