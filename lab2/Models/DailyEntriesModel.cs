using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class DailyEntriesModel
    {
        public class ExtendedEntry {
            public Entry inner;
            public bool modifiable;
        }
        public DailyEntriesModel(String user) : this(user, DateTime.Today) {}

        public DailyEntriesModel(String user, DateTime date) {
            this.SelectedDate = date;
            var users = Users.load();
            foreach (var u in users) {
                var report = Report.load(u, date);
                foreach (var entry in report.entries) {
                    if (entry.date.Date.CompareTo(date) == 0) {
                        this.Entries.Add(new ExtendedEntry {
                            inner = entry,
                            modifiable = u.Equals(user) & !report.frozen,
                        });
                    }
                }
            }
            this.Entries.ForEach(e =>
            {
                this.TotalTime += e.inner.time;
            });
        }

        public DateTime SelectedDate;

        public List<ExtendedEntry> Entries = new List<ExtendedEntry>();

        public int TotalTime = 0;
    }
}
