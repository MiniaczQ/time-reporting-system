using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    //public class OverseeActivityModel
    //{
    //    public class UserReports
    //    {
    //        public string user;
    //        public List<DateReport> reports;
    //    }
    //    public class DateReport
    //    {
    //        public DateTime date;
    //        public List<CodeEntries> report;
    //    }
    //    public class CodeEntries
    //    {
    //        public string code;
    //        public int budget;
    //        public List<ReducedEntry> entries;
    //    }
    //    public class ReducedEntry
    //    {
    //        public string subcode;
    //        public int time;
    //        public string description;
    //    }
    //
    //    // \/ \/ \/ Lots of mapping because JSON \/ \/ \/
    //    ReducedEntry IntoReducedEntry(Entities.Entry entry)
    //    {
    //        return new ReducedEntry
    //        {
    //            subcode = entry.Subcode,
    //            time = entry.Time,
    //            description = entry.Description,
    //        };
    //    }
    //
    //    List<ReducedEntry> IntoReducedEntryList(string code, Entities.Report report)
    //    {
    //        return report.Entries.Select(entry =>
    //        {
    //            if (entry.Code.Equals(code))
    //            {
    //                return IntoReducedEntry(entry);
    //            }
    //            return null;
    //        }).Where(x => x != null).ToList();
    //    }
    //
    //    CodeEntries IntoCodeEntries(string code, Entities.Report report)
    //    {
    //        var entries = IntoReducedEntryList(code, report);
    //        if (entries != null && entries.Count > 0)
    //        {
    //            int budget = 0;
    //            var acceptedEntry = report.AcceptedEntries.Find(entry => entry.code.Equals(code));
    //            if (acceptedEntry != null)
    //            {
    //                budget = acceptedEntry.time;
    //            }
    //            return new CodeEntries
    //            {
    //                code = code,
    //                budget = budget,
    //                entries = entries,
    //            };
    //        }
    //        return null;
    //    }
    //
    //    List<CodeEntries> IntoCodeEntriesList(Entities.Report report, string inCode, List<String> codes)
    //    {
    //        return codes.Select(code =>
    //        {
    //            var codeEntries = IntoCodeEntries(code, report);
    //            if (code.Equals(inCode) && codeEntries != null && codeEntries.entries.Count > 0)
    //            {
    //                return codeEntries;
    //            }
    //            return null;
    //        }).Where(x => x != null).ToList();
    //    }
    //
    //    DateReport IntoDateReport(DateTime date, Entities.Report inReport, string code, List<String> codes)
    //    {
    //        var report = IntoCodeEntriesList(inReport, code, codes);
    //        if (report != null && report.Count > 0)
    //        {
    //            return new DateReport
    //            {
    //                date = date,
    //                report = report,
    //            };
    //        }
    //        return null;
    //    }
    //    List<DateReport> IntoDateReportList(List<Report.DateReport> inDateReports, string code, List<String> codes)
    //    {
    //        var dateReports = inDateReports.Select(inDateReport =>
    //        {
    //            var dateReport = IntoDateReport(inDateReport.date, inDateReport.report, code, codes);
    //            return dateReport;
    //        }).Where(x => x != null).ToList();
    //        return dateReports;
    //    }
    //    UserReports IntoUserReports(string user, string code, List<String> codes)
    //    {
    //        var reports = IntoDateReportList(Entities.Report.getAllForUser(user), code, codes);
    //        if (reports.Count > 0)
    //        {
    //            return new UserReports
    //            {
    //                user = user,
    //                reports = reports,
    //            };
    //        }
    //        return null;
    //    }
    //    // /\ /\ /\ Lots of mapping because JSON /\ /\ /\
    //    public OverseeActivityModel(string code, bool active)
    //    {
    //        this.code = code;
    //        this.active = active;
    //        var users = Entities.Users.load();
    //        var codes = Entities.Activities.load().activities.Select(a => a.code).ToList();
    //        this.usersReports = users.Select(user => IntoUserReports(user, code, codes)).Where(x => x != null).ToList();
    //    }
    //
    //    public List<UserReports> usersReports;
    //    public bool active;
    //    public string code;
    //}
}
