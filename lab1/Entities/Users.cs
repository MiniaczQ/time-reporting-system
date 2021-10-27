using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Entities
{
    public class Users
    {
        public static HashSet<String> load()
        {
            var users_json = System.IO.File.ReadAllText("database/users.json");
            return System.Text.Json.JsonSerializer.Deserialize<HashSet<String>>(users_json);
        }

        public static void save(HashSet<String> users_list)
        {
            var json_options = new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented = true };
            var bytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(users_list, json_options);
            System.IO.File.WriteAllBytes("database/users.json", bytes);
        }
    }
}
