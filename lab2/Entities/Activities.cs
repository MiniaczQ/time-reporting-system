using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Entities
{
    public class Activity
    {
        public String code {get; set;}
        public String manager {get; set;}
        public String name {get; set;}
        public int budget {get; set;}
        public bool active {get; set;}
        public List<Subcode> subactivities {get; set;}
    }

    public class Subcode
    {
        public String code {get; set;}
    }

    public class Activities
    {
        public List<Activity> activities { get; set; }
        public static Activities load()
        {
            var path = "database/activities.json";
            if (!System.IO.File.Exists(path)) {
                return new Activities();
            }
            var activities_json = System.IO.File.ReadAllText(path);
            return System.Text.Json.JsonSerializer.Deserialize<Activities>(activities_json);
        }

        public static void save(Activities activities)
        {
            var path = "database/activities.json";
            var json_options = new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(activities, json_options);
            System.IO.File.WriteAllBytes(path, bytes);
        }
    }
}
