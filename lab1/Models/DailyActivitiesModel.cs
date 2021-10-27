using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class DailyActivitiesModel
    {
        public DailyActivitiesModel() {
            this.SelectedDate = DateTime.Today.ToLongDateString();
        }

        public DailyActivitiesModel(DateTime date) {
            this.SelectedDate = date.ToLongDateString();
        }

        public String SelectedDate;

        public List<String> Activities;
    }
}
