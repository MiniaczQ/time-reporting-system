using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

using lab1.Entities;

namespace lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new LabContext())
            {
                var available = db.Database.CanConnect();
                if (!available)
                    throw new Exception("Database unavailable!");
                db.Database.EnsureCreated();
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
