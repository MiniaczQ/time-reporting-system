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
            var username = Request.Cookies["user"];
            if (username == null)
                return RedirectToAction("Login", "User");
            if (date.HasValue)
                return View(new MyEntriesModel(username, date.Value));
            else
                return View(new MyEntriesModel(username));
        }

        [HttpGet]
        public IActionResult ModifyEntry(Entry entry)
        {
            return View("UpsertEntry", new UpsertEntryModel(entry));
        }

        [HttpPost]
        public IActionResult PostModifyEntry(UpsertEntryModel entryModel)
        {
            var entry = entryModel.Entry;
            System.Console.WriteLine(entry.SubactivityCode);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
