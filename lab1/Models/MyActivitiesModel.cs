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
            this.Activities = Entities.Activities.load();
            this.Activities.activities.RemoveAll(a => a.manager != user);
        }

        public Activities Activities;
    }
}
