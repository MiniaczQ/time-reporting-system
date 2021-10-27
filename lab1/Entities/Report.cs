using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Entities
{
    public class Entry
    {
        public String date;
        public String code;
        public String subcode;
        public int time;
        public String description;
    }

    public class AcceptedEntry
    {
        public String code;
        public int time;
    }

    public class Report
    {
        public bool frozen;
        public List<Entry> entries;
        public List<AcceptedEntry> acceptedEntries;

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
