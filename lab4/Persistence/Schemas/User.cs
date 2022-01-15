using System;
using System.Collections.Generic;

namespace lab4.Persistence.Schemas
{
    public class User
    {
        public string UserName { get; set; }


        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
