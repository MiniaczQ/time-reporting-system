using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lab1.Models;
using Microsoft.AspNetCore.Http;
using lab1.Entities;
using Microsoft.EntityFrameworkCore;

namespace lab1.Controllers
{
    public class ActivityController : Controller
    {
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(ILogger<ActivityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult MyActivities()
        {
            var username = Request.Cookies["user"];
            if (username == null)
                return RedirectToAction("Login", "User");
            return View(new MyActivitiesModel(username));
        }

        [HttpGet]
        public IActionResult AddActivity()
        {
            return View(new AddActivityModel());
        }

        [HttpPost]
        public IActionResult PostAddActivity(AddActivityModel activityModel)
        {
            var username = Request.Cookies["user"];
            if (username == null)
                return RedirectToAction("Login", "User");
            var activity = activityModel.Activity;
            activity.ManagerName = username;
            try
            {
                using (var db = new LabContext())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        db.Add(activity);
                        db.SaveChanges();
                        db.AddRange(activityModel.SubcodesString.Split(',').Select(s => s.Trim()).Select(s => new Subcode { ActivityCode = activity.ActivityCode, SubactivityCode = s }));
                        db.SaveChanges();
                        trans.Commit();
                    }
                }
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("MyActivities");
            }
            return RedirectToAction("MyActivities");
        }

        public JsonResult Subcodes(string ActivityCode)
        {
            Activity activity;
            using (var db = new LabContext())
            {
                activity = db.Activities.Include(a => a.Subcodes).Where(a => a.ActivityCode == ActivityCode).FirstOrDefault();
            }
            if (activity == null)
                return Json(null);
            return Json(activity.Subcodes.Select(s => s.SubactivityCode).Prepend("").ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
