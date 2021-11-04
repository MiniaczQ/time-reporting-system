using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lab1.Models;
using Microsoft.AspNetCore.Http;

namespace lab1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = Request.Cookies["user"];
            if (user != null) {
                return Redirect("/Home/DailyEntries");
            } else {
                return Redirect("/Home/UserSelect");
            }
        }

        public IActionResult Logout()
        {
            var cookie_options = new CookieOptions { HttpOnly = true, Secure = false, Expires = DateTime.UnixEpoch };
            Response.Cookies.Append("user", "", cookie_options);
            return Redirect("./UserSelect");
        }

        [HttpGet]
        public IActionResult UserSelect(bool? invalid)
        {
            if (invalid.HasValue) {
                return View(new UserSelectModel(invalid.Value));
            } else {
                return View(new UserSelectModel());
            }
        }

        [HttpPost]
        public IActionResult UserSelect(String user, String type)
        {
            if (type == "New user")
            {
                var users_list = lab1.Entities.Users.load();
                if (!users_list.Add(user)) {
                    return Redirect("UserSelect?invalid=true");
                }
                lab1.Entities.Users.save(users_list);
            }
            var cookie_options = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("user", user, cookie_options);

            return Redirect("/Home/DailyEntries");
        }

        [HttpGet]
        public IActionResult DailyEntries(DateTime? date)
        {
            if (date.HasValue) {
                return View(new DailyEntriesModel(date.Value));
            } else {
                return View(new DailyEntriesModel());
            }
        }

        public IActionResult AddEntry()
        {
            return View(new AddEntryModel());
        }

        [HttpPost]
        public IActionResult AddEntry(DateTime date, String code, String subcode, int time, String description)
        {
            var user = Request.Cookies["user"];
            if (user != null)
            {
                var entry = new Entities.Entry(date, code, subcode, time, description);
                var report = Entities.Report.load(user, date);
                report.entries.Add(entry);
                Entities.Report.save(report, user, date);
                return Redirect($"./DailyEntries?date={date.Year}-{date.Month}-{date.Day}");
            }
            return Redirect("./Index");
        }

        public JsonResult GetSubcodes(String code) {
            var activities = Entities.Activities.load();
            var subactivities = activities.activities.Find(e => e.code.Equals(code)).subactivities;
            var subcodes = subactivities.Select(e => e.code).ToList();
            subcodes.Insert(0, "");
            return Json(subcodes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
