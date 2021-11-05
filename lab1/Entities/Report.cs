using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Entities
{
    public class Entry
    {
        public Entry(DateTime date, String code, String subcode, int time, String description)
        {
            this.date = date;
            this.code = code;
            this.subcode = subcode;
            this.time = time;
            this.description = description;
        }
        [DataType(DataType.Date)]
        public DateTime date {get; set;}
        public String code {get; set;}
        public String subcode {get; set;}
        public int time {get; set;}
        public String description {get; set;}

        public static List<Entry> getAllAt(DateTime date) {
            List<Entry> entries = new ();
            var users = Users.load();
            foreach (var user in users) {
                var report = Report.load(user, date);
                foreach (var entry in report.entries) {
                    if (entry.date.Date.CompareTo(date) == 0) {
                        entries.Add(entry);
                    }
                }
            }
            return entries;
        }

        public static Tuple<String, int, Entry> getOneAt(DateTime date, int index) {
            var i = 0;
            var users = Entities.Users.load();
            foreach (var user in users)
            {
                var report = Entities.Report.load(user, date);
                var j = 0;
                foreach (var entry in report.entries)
                {
                    if (entry.date.Date.CompareTo(date) == 0)
                    {
                        i++;
                    }
                    if (i == index)
                    {
                        return Tuple.Create(user, j, entry);
                    }
                    j++;
                }
            }
            return null;
        }
        public static Tuple<int, String> Locate(DateTime date, int index)
        {
            var i = 0;
            var users = Entities.Users.load();
            foreach (var user in users)
            {
                var report = Entities.Report.load(user, date);
                var j = 0;
                foreach (var entry in report.entries)
                {
                    if (entry.date.Date.CompareTo(date) == 0)
                    {
                        i++;
                    }
                    if (i == index)
                    {
                        return Tuple.Create(j, user);
                    }
                    j++;
                }
            }
            return null;
        }

        public static void Remove(int index, String user, DateTime date)
        {
            var report = Entities.Report.load(user, date);
            report.entries.RemoveAt(index);
            Entities.Report.save(report, user, date);
        }
    }

    public class AcceptedEntry
    {
        public String code {get; set;}
        public int time {get; set;}
    }

    public class Report
    {
        public bool frozen {get; set;} = false;
        public List<Entry> entries {get; set;} = new ();
        public List<AcceptedEntry> acceptedEntries {get; set;} = new ();

        public static Report load(String user, DateTime date)
        {
            var path = $"database/{user}-{date.Year}-{date.Month}.json";
            if (!System.IO.File.Exists(path)) {
                return new Report();
            }
            var report_json = System.IO.File.ReadAllText(path);
            return System.Text.Json.JsonSerializer.Deserialize<Report>(report_json);
        }

        public static void save(Report report, String user, DateTime date)
        {
            var path = $"database/{user}-{date.Year}-{date.Month}.json";
            var json_options = new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(report, json_options);
            System.IO.File.WriteAllBytes(path, bytes);
        }

        public static List<DateTime> getAllDatesForUser(String user)
        {
            var allFiles = System.IO.Directory.GetFiles("database");
            var reCheck = new System.Text.RegularExpressions.Regex(user + @"-[0-9]{4}-[0-9]{2}\.json");
            var reExtractDate = new System.Text.RegularExpressions.Regex("[0-9]{4}-[0-9]{2}", System.Text.RegularExpressions.RegexOptions.RightToLeft);
            return allFiles.Where(e => reCheck.IsMatch(e)).Select(e => reExtractDate.Match(e).Value).Select(e => DateTime.ParseExact(e, "yyyy-MM", null)).ToList();
        }

        public static List<Report> getAll()
        {
            var allFiles = System.IO.Directory.GetFiles("database");
            var reCheck = new System.Text.RegularExpressions.Regex(@".*?-[0-9]{4}-[0-9]{2}\.json");
            var reExtractDate = new System.Text.RegularExpressions.Regex("[0-9]{4}-[0-9]{2}", System.Text.RegularExpressions.RegexOptions.RightToLeft);
            return allFiles
                .Where(e => reCheck.IsMatch(e))
                .Select(e =>
                {
                    var v = reCheck.Match(e).Value;
                    var user = v.Substring(9, v.Length - 22);
                    var date = DateTime.ParseExact(reExtractDate.Match(e).Value, "yyyy-MM", null);
                    return load(user, date);
                }).ToList();
        }

        public class DateReport {
            public DateTime date;
            public Report report;
        }
        public static List<DateReport> getAllForUser(String user)
        {
            var dates = getAllDatesForUser(user);
            return dates.Select(e => new DateReport {
                date = e,
                report = load(user, e),
            }).ToList();
        }
    }
}
