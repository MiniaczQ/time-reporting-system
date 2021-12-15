using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lab1.Models;
using Microsoft.AspNetCore.Http;
using lab1.Entities;

namespace lab1.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Login path
        /// Invalid flag is for failed attempt at adding an user
        /// </summary>
        [HttpGet]
        public IActionResult Login(bool? UserAlreadyExists, bool? LoggedOut)
        {
            return View(new LoginModel(UserAlreadyExists, LoggedOut));
        }

        /// <summary>
        /// Login request
        /// If `type` is set to `New user`, it will also attempt to add it before logging in
        /// </summary>
        [HttpPost]
        public IActionResult Login(String username, String type)
        {
            var user = new User { UserName = username };

            if (type.Equals("new-user"))
                try
                {
                    using (var db = new LabContext())
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                }
                catch (System.Exception)
                {
                    return Redirect("./Login?UserAlreadyExists=true");
                }

            var cookie_options = new CookieOptions { HttpOnly = false, Secure = false, MaxAge = TimeSpan.FromMinutes(15) };
            Response.Cookies.Append("user", user.UserName, cookie_options);

            return Redirect("/Home/DailyEntries");
        }

        /// <summary>
        /// Logout, redirects to login
        /// </summary>
        public IActionResult Logout()
        {
            var cookie_options = new CookieOptions { HttpOnly = true, Secure = false, Expires = DateTime.UnixEpoch };
            Response.Cookies.Append("user", "", cookie_options);

            return Redirect("./Login?LoggedOut=true");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
