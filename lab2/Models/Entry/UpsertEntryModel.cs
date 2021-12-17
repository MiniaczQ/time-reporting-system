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
                this.Activities = db.Activities.Select(a => new SelectListItem(a.ActivityName, a.ActivityCode)).ToList();
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
        }

        public Entry Entry { get; set; }
        public List<SelectListItem> Activities { get; set; }
        public bool Insert { get; set; }
    }
}
