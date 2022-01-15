using System;
using System.Collections.Generic;

namespace lab4.Persistence.Schemas
{
    public class Project
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int Budget { get; set; }
        public bool Active { get; set; } = true;


        public string ManagerName { get; set; }
        public virtual User Manager { get; set; }


        public virtual ICollection<AcceptedActivity> AcceptedActivities { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Subproject> Subprojects { get; set; }
    }
}
