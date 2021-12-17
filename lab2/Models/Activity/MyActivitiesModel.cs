using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lab1.Models
{
    public class MyActivitiesModel
    {
        public MyActivitiesModel(string username)
        {
            using (var db = new LabContext())
            {
                this.Activities = db.Activities
                    .Where(a => a.ManagerName == username)
                    .Include(a => a.Subcodes)
                    .Include(a => a.Entries)
                    .Include(a => a.AcceptedEntries)
                    .ToList();
            }
        }
        public List<Activity> Activities { get; set; }
    }
}
