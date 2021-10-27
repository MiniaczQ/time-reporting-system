using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lab1.Models;
using Microsoft.AspNetCore.Http;

namespace lab1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserSelect(bool? invalid)
        {
            if (invalid.HasValue) {
                return View(new UserSelectModel(invalid.Value));
            } else {
                return View(new UserSelectModel());
            }
        }

        [HttpPost]
        public IActionResult UserSelect(String user, String type)
        {
            if (type == "New user")
            {
                var users_list = lab1.Entities.Users.load();
                if (!users_list.Add(user)) {
                    return Redirect("UserSelect?invalid=true");
                }
                lab1.Entities.Users.save(users_list);
            }
            var cookie_options = new CookieOptions { HttpOnly = true, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("user", user, cookie_options);

            return Redirect("/Home/DailyActivities");
        }

        [HttpGet]
        public IActionResult DailyActivities(DateTime? date)
        {
            if (date.HasValue) {
                return View(new DailyActivitiesModel(date.Value));
            } else {
                return View(new DailyActivitiesModel());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
