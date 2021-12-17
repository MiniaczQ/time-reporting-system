using System;
using System.Collections.Generic;

namespace lab1.Entities
{
    public class User
    {
        // Key
        public string UserName { get; set; }

        // Children
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
