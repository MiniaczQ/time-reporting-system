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
            this.SelectedDate = date;
            this.Entries = Entities.Entry.getAllAt(date);
            this.Entries.ForEach(e =>
            {
                this.TotalTime += e.time;
            });
        }

        public DateTime SelectedDate;

        public List<Entry> Entries = new List<Entry>();

        public int TotalTime = 0;
    }
}
