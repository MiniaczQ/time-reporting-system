using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            using (var db = new LabContext())
            {
                var users = db.Users.ToList();
                this.UserList.AddRange(users.Select(u => new SelectListItem(u.UserName, u.UserName)));
            }
        }
        public LoginModel(bool? UserExists, bool? LoggedOut) : this()
        {
            if (UserExists.HasValue && UserExists.Value)
            {
                this.Message = "User already exists.";
                return;
            }
            if (LoggedOut.HasValue && LoggedOut.Value)
            {
                this.Message = "You've been logged out.";
                return;
            }
        }

        public List<SelectListItem> UserList = new();
        public string Message = "";
    }
}
