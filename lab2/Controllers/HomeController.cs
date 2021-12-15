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
    // podzielić na kilka:
    // rejstracja czasu pracy,
    // obsługa raportów,
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Goto path, redirect depending on being logged in
        /// </summary>
        public IActionResult Index()
        {
            var user = Request.Cookies["user"];
            if (user != null) {
                return Redirect("/Home/DailyEntries");
            } else {
                return Redirect("/Home/UserSelect");
            }
        }

        /// <summary>
        /// Logout, redirects to login
        /// </summary>
        public IActionResult Logout()
        {
            var cookie_options = new CookieOptions { HttpOnly = true, Secure = false, Expires = DateTime.UnixEpoch };
            Response.Cookies.Append("user", "", cookie_options);
            return Redirect("./UserSelect");
        }

        /// <summary>
        /// Login path
        /// Invalid flag is for failed attempt at adding an user
        /// </summary>
        [HttpGet]
        public IActionResult UserSelect(bool? invalid)
        {
            if (invalid.HasValue) {
                return View(new UserSelectModel(invalid.Value));
            } else {
                return View(new UserSelectModel());
            }
        }

        /// <summary>
        /// Login request
        /// If `type` is set to `New user`, it will also attempt to add it before logging in
        /// </summary>
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

        /// <summary>
        /// All entries registered for a given day
        /// </summary>
        [HttpGet]
        public IActionResult DailyEntries(DateTime? date)
        {
            var user = Request.Cookies["user"];
            if (user == null) {
                return Redirect("/Home/UserSelect");
            }
            if (date.HasValue) {
                return View(new DailyEntriesModel(user, date.Value));
            } else {
                return View(new DailyEntriesModel(user));
            }
        }

        /// <summary>
        /// Entry modification page
        /// `date` and `index` are used to back-track the entry to it's origin JSON
        /// </summary>
        public IActionResult ModifyEntry(DateTime date, int index)
        {
            var model = new ModifyEntryModel(date, index);
            return View(model);
        }

        /// <summary>
        /// Request for modifying an entry
        /// Redirects back to daily entries page
        /// </summary>
        [HttpPost]
        public IActionResult ModifyEntry(DateTime date, String subcode, int time, String description, String user, int index)
        {
            var report = Entities.Report.load(user, date);
            var entry = report.entries[index];

            // Replace entry in the JSON
            report.entries.RemoveAt(index);
            report.entries.Add(entry);
            entry.time = time;
            entry.subcode = subcode;
            entry.description = description;
            Entities.Report.save(report, user, date);

            return Redirect("./DailyEntries");
        }

        /// <summary>
        /// Delete entry request
        /// Redirects back to daily entries page
        /// </summary>
        [HttpPost]
        public IActionResult DeleteEntry(DateTime date, int index)
        {
            var result = Entities.Entry.Locate(date, index);

            Entities.Entry.Remove(result.Item1, result.Item2, date);

            return Redirect($"./DailyEntries?date={date.Year}-{date.Month}-{date.Day}");
        }

        /// <summary>
        /// Info page about a specific Entry
        /// `date` and `index` used for back-tracking like in `AddEntry`
        /// </summary>
        public IActionResult EntryInfo(DateTime date, int index)
        {
            return View(new EntryInfoModel(date, index));
        }

        /// <summary>
        /// All activities of the current user
        /// Redirects to login if not logged in
        /// </summary>
        public IActionResult MyActivities()
        {
            var user = Request.Cookies["user"];
            if (user == null) {
                return Redirect("/Home/UserSelect");
            }
            return View(new MyActivitiesModel(user));
        }

        /// <summary>
        /// Closes activity with the specified code
        /// </summary>
        public IActionResult CloseActivity(String code)
        {
            // Replacing the value in JSON
            var activities = Entities.Activities.load();
            var activity = activities.activities.Find(a => a.code.Equals(code));
            activities.activities.RemoveAll(a => a.code.Equals(code));
            activity.active = false;
            activities.activities.Add(activity);
            Entities.Activities.save(activities);

            return RedirectToAction("MyActivities");
        }

        /// <summary>
        /// Modifies the decided budget for user-month entry groups
        /// Redirects back to the same page
        /// </summary>
        public IActionResult SetActivityTimeBudget(String code, DateTime date, int budget, String user)
        {
            var report = Entities.Report.load(user, date);
            report.acceptedEntries.RemoveAll(e => e.code.Equals(code));
            report.acceptedEntries.Add(new Entities.AcceptedEntry
            {
                code = code,
                time = budget,
            });
            Entities.Report.save(report, user, date);
            return Redirect($"/Home/OverseeActivity?code={code}&active=true");
        }

        /// <summary>
        /// Look over all entries for a given activity
        /// If activity is active, allow for modification of budget per user-month
        /// </summary>
        public IActionResult OverseeActivity(String code, bool active)
        {
            var user = Request.Cookies["user"];
            if (user == null) {
                return Redirect("/Home/UserSelect");
            }
            return View(new OverseeActivityModel(code, active));
        }

        /// <summary>
        /// List the time used up per project for a specific month
        /// If not logged in, redirect to log in
        /// </summary>
        public IActionResult MonthlySummary(DateTime? date)
        {
            var user = Request.Cookies["user"];
            if (user == null) {
                return Redirect("/Home/UserSelect");
            }
            if (date.HasValue) {
                return View(new MonthlySummaryModel(user, date.Value));
            } else {
                return View(new MonthlySummaryModel(user));
            }
        }

        /// <summary>
        /// Form for adding a new activity
        /// </summary>
        public IActionResult AddActivity()
        {
            return View();
        }

        /// <summary>
        /// Lock a specific month of entries for the logged in user
        /// Redirects to log in if not logged in
        /// </summary>
        [HttpPost]
        public IActionResult LockMonth(DateTime date)
        {
            var user = Request.Cookies["user"];
            if (user == null)
            {
                return Redirect("/Home/UserSelect");
            }
            var report = Entities.Report.load(user, date);
            report.frozen = true;
            Entities.Report.save(report, user, date);
            return RedirectToAction("MonthlySummary");
        }

        /// <summary>
        /// Request for adding an activity
        /// </summary>
        [HttpPost]
        public IActionResult AddActivity(String name, String code, String subactivities, int budget)
        {
            var user = Request.Cookies["user"];
            if (user == null)
            {
                return Redirect("/Home/UserSelect");
            }
            var activities = Entities.Activities.load();
            if (activities.activities.Any(e => e.code.Equals(code)))
            {
                return RedirectToAction("DailyEntries");
            }
            var sactivities = subactivities.Split(' ').Select(e => new Entities.Subcode {code=e}).ToList();
            var activity = new Entities.Activity
            {
                name = name,
                code = code,
                subactivities = sactivities,
                budget = budget,
                manager = user,
                active = true,
            };
            activities.activities.Add(activity);
            Entities.Activities.save(activities);
            return RedirectToAction("DailyEntries");
        }

        /// <summary>
        /// New entry form
        /// </summary>
        public IActionResult AddEntry(DateTime? date)
        {
            return View(new AddEntryModel(date));
        }

        /// <summary>
        /// Request for adding a new entry
        /// </summary>
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

        /// <summary>
        /// Returns a list of subcodes for a given code (activity)
        /// Used by JS to dynamically swap cascading select input
        /// </summary>
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
