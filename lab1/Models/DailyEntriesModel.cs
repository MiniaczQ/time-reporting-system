using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class DailyEntriesModel
    {
        public DailyEntriesModel() : this(DateTime.Today) {}

        public DailyEntriesModel(DateTime date) {
            this.SelectedDate = date.ToLongDateString();
            var users = Users.load();
            foreach (var user in users) {
                var report = Report.load(user, date);
                foreach (var entry in report.entries) {
                    if (entry.date.Date.CompareTo(date) == 0) {
                        this.Entries.Add(entry);
                        this.TotalTime += entry.time;
                    }
                }
            }
        }

        public String SelectedDate;

        public List<Entry> Entries = new List<Entry>();

        public int TotalTime = 0;
    }
}
