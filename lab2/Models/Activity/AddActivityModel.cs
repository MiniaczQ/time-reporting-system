using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class AddActivityModel
    {
        public Activity Activity { get; set; }
        public string SubcodesString { get; set; }
    }
}
