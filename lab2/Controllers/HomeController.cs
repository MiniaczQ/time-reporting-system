        //public IActionResult CloseActivity(string code)
        //{
        //    // Replacing the value in JSON
        //    var activities = Entities.Activities.load();
        //    var activity = activities.activities.Find(a => a.code.Equals(code));
        //    activities.activities.RemoveAll(a => a.code.Equals(code));
        //    activity.active = false;
        //    activities.activities.Add(activity);
        //    Entities.Activities.save(activities);
        //
        //    return RedirectToAction("MyActivities");
        //}

        //public IActionResult SetActivityTimeBudget(string code, DateTime date, int budget, string user)
        //{
        //    var report = Entities.Report.load(user, date);
        //    report.acceptedEntries.RemoveAll(e => e.code.Equals(code));
        //    report.acceptedEntries.Add(new Entities.AcceptedEntry
        //    {
        //        Code = code,
        //        Time = budget,
        //    });
        //    Entities.Report.save(report, user, date);
        //    return Redirect($"/Home/OverseeActivity?code={code}&active=true");
        //}

        //public IActionResult OverseeActivity(string code, bool active)
        //{
        //    var user = Request.Cookies["user"];
        //    if (user == null) {
        //        return Redirect("/Home/UserSelect");
        //    }
        //    return View(new OverseeActivityModel(code, active));
        //}

        //public IActionResult MonthlySummary(DateTime? date)
        //{
        //    var user = Request.Cookies["user"];
        //    if (user == null) {
        //        return Redirect("/Home/UserSelect");
        //    }
        //    if (date.HasValue) {
        //        return View(new MonthlySummaryModel(user, date.Value));
        //    } else {
        //        return View(new MonthlySummaryModel(user));
        //    }
        //}

        //[HttpPost]
        //public IActionResult LockMonth(DateTime date)
        //{
        //    var user = Request.Cookies["user"];
        //    if (user == null)
        //    {
        //        return Redirect("/Home/UserSelect");
        //    }
        //    var report = Entities.Report.load(user, date);
        //    report.frozen = true;
        //    Entities.Report.save(report, user, date);
        //    return RedirectToAction("MonthlySummary");
        //}