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
        public DateTime date {get; set;}
        public String code {get; set;}
        public String subcode {get; set;}
        public int time {get; set;}
        public String description {get; set;}
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
    }
}
