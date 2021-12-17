using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class UpsertEntryModel
    {
        public UpsertEntryModel()
        {
            this.Entry = new Entry();
            using (var db = new LabContext())
            {
                this.Activities = db.Activities
                    .Where(a => a.Active)
                    .Select(a => new SelectListItem(a.ActivityName, a.ActivityCode))
                    .ToList();
            }
            this.Insert = true;
        }

        public UpsertEntryModel(DateTime date) : this()
        {
            this.Entry.Date = date;
        }

        public UpsertEntryModel(Entry entry)
        {
            this.Entry = entry;
            this.Activities = new();
            this.Insert = false;
            using (var db = new LabContext())
            {
                this.Subcodes = db.Subcodes.Where(s => s.ActivityCode == entry.ActivityCode).Select(s => s.SubactivityCode).AsEnumerable().Prepend("").Select(s => new SelectListItem(s, s)).ToList();
            }
        }

        public Entry Entry { get; set; }
        public List<SelectListItem> Activities { get; set; }
        public List<SelectListItem> Subcodes { get; set; } = new();
        public bool Insert { get; set; }
    }
}
