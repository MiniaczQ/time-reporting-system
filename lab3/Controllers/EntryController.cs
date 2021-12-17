using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lab1.Models;
using Microsoft.AspNetCore.Http;
using lab1.Entities;
using Microsoft.EntityFrameworkCore;
using lab1.Datetime;
using lab1.debug;

namespace lab1.Controllers
{
    public class EntryController : Controller
    {
        private readonly ILogger<EntryController> _logger;

        public EntryController(ILogger<EntryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult MyEntries(DateTime? date)
        {
            var UserName = Request.Cookies["user"];
            if (UserName == null)
                return RedirectToAction("Login", "User");
            if (date.HasValue)
                return View(new MyEntriesModel(UserName, date.Value));
            else
                return View(new MyEntriesModel(UserName));
        }

        [HttpGet]
        public IActionResult EntryInfo(MyEntriesModel entryModel)
        {
            var UserName = Request.Cookies["user"];
            if (UserName == null)
                return RedirectToAction("Login", "User");

            var entry = entryModel.SelectedEntry;
            entry.UserName = UserName;

            Entry e;
            using (var db = new LabContext())
            {
                e = db.Entries
                    .Where(e
                        => e.UserName == entry.UserName
                        && e.ReportMonth == entry.ReportMonth
                        && e.ActivityCode == entry.ActivityCode
                        && e.EntryPid == entry.EntryPid)
                    .Include(e => e.Activity)
                    .FirstOrDefault();
            }
            return View(e);
        }

        [HttpGet]
        public IActionResult ModifyEntry(MyEntriesModel entryModel)
        {
            var UserName = Request.Cookies["user"];
            if (UserName == null)
                return RedirectToAction("Login", "User");

            var entry = entryModel.SelectedEntry;
            entry.UserName = UserName;

            Entry e;
            using (var db = new LabContext())
            {
                e = db.Entries
                    .Where(e
                        => e.UserName == entry.UserName
                        && e.ReportMonth == entry.ReportMonth
                        && e.ActivityCode == entry.ActivityCode
                        && e.EntryPid == entry.EntryPid)
                    .Include(e => e.Activity)
                    .FirstOrDefault();
            }
            return View("UpsertEntry", new UpsertEntryModel(e));
        }

        [HttpPost]
        public IActionResult PostModifyEntry(UpsertEntryModel entryModel)
        {
            var UserName = Request.Cookies["user"];
            if (UserName == null)
                return RedirectToAction("Login", "User");

            var entry = entryModel.Entry;
            entry.UserName = UserName;
            entry.Activity = null;

            using (var db = new LabContext())
            {
                db.Update(entry);
                db.SaveChanges();
            }
            return RedirectToAction("MyEntries");
        }

        [HttpGet]
        public IActionResult AddEntry(DateTime? date)
        {
            return View("UpsertEntry", new UpsertEntryModel(date.HasValue ? date.Value : DateTime.Now));
        }

        [HttpPost]
        public IActionResult PostAddEntry(UpsertEntryModel entryModel)
        {
            var UserName = Request.Cookies["user"];
            if (UserName == null)
                return RedirectToAction("Login", "User");

            var entry = entryModel.Entry;
            entry.UserName = UserName;

            var ReportMonth = entry.Date.Dayless();
            using (var db = new LabContext())
            {
                if (!db.Reports.Any(r => r.ReportMonth == ReportMonth && r.UserName == UserName))
                {
                    db.Add(new Report { ReportMonth = ReportMonth, UserName = UserName });
                    db.SaveChanges();
                }
            }
            entry.ReportMonth = ReportMonth;
            try
            {
                using (var db = new LabContext())
                {
                    db.Add(entry);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                System.Console.WriteLine(e.ToString());
                return RedirectToAction("MyEntries");
            }
            return RedirectToAction("MyEntries");
        }

        [HttpPost]
        public IActionResult DeleteEntry(MyEntriesModel entriesModel)
        {
            var UserName = Request.Cookies["user"];
            if (UserName == null)
                return RedirectToAction("Login", "User");

            var entry = entriesModel.SelectedEntry;
            entry.UserName = UserName;

            using (var db = new LabContext())
            {
                db.Remove(entry);
                db.SaveChanges();
            }
            return RedirectToAction("MyEntries");
        }

        [HttpGet]
        public IActionResult MonthlySummary(DateTime? dayless)
        {
            var UserName = Request.Cookies["user"];
            if (UserName == null)
                return RedirectToAction("Login", "User");
            var Date = dayless ?? DateTime.Now;

            return View(new MonthlySummaryModel(Date, UserName));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult LockMonth(DateTime dayless)
        {
            var UserName = Request.Cookies["user"];
            if (UserName == null)
                return RedirectToAction("Login", "User");

            var ReportMonth = dayless.Dayless();
            var report = new Report { ReportMonth = ReportMonth, UserName = UserName, Frozen = true };
            try
            {
                using (var db = new LabContext())
                {
                    db.Update(report);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateException)
            { }
            return RedirectToAction("MonthlySummary", new { dayless = dayless });
        }
    }
}
