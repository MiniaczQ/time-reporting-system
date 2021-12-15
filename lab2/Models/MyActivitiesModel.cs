using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class MyActivitiesModel
    {
        public MyActivitiesModel(String user) {
            var activities = Entities.Activities.load();
            activities.activities.RemoveAll(a => a.manager != user);
            var reports = Entities.Report.getAll();
            foreach (var report in reports)
            {
                foreach (var entry in report.acceptedEntries)
                {
                    foreach (var activity in activities.activities) {
                        if (entry.code.Equals(activity.code)) {
                            activity.budget -= entry.time;
                        }
                    }
                }
            }
            this.Activities = activities;
        }

        public Activities Activities;
    }
}
