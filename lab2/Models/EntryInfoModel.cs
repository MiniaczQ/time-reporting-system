using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class EntryInfoModel
    {
        public EntryInfoModel(DateTime date, int index) {
            var result = Entities.Entry.getOneAt(date, index);
            this.user = result.Item1;
            this.index = result.Item2;
            this.entry = result.Item3;
            this.activity = Entities.Activities.load().activities.Find(e =>
                e.code == this.entry.code
            );
        }
        public String user;
        public int index;
        public Entry entry;
        public Entities.Activity activity;
    }
}
