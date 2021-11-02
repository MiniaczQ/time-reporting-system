using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Entities
{
    public class Activity
    {
        public String code;
        public String manager;
        public String name;
        public int budget;
        public bool active;
        public List<String> subactivities;
    }

    public class Activities
    {
        public static List<Activity> load()
        {
            var path = "database/activities.json";
            if (!System.IO.File.Exists(path)) {
                return new List<Activity>();
            }
            var activities_json = System.IO.File.ReadAllText(path);
            return System.Text.Json.JsonSerializer.Deserialize<List<Activity>>(activities_json);
        }

        public static void save(List<Activity> activities)
        {
            var path = "database/activities.json";
            var json_options = new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(activities, json_options);
            System.IO.File.WriteAllBytes(path, bytes);
        }
    }
}
