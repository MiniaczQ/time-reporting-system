using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Entities
{
    public class Activity
    {
        public String code;
        public String manager;
        public String name;
        public int budget;
        public bool active;
        public List<String> subactivities;
    }
}
