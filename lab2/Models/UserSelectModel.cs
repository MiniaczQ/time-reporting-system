using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab1.Models
{
    public class UserSelectModel
    {
        public UserSelectModel(bool invalid)
        {
            this.invalid = true;
        }

        public UserSelectModel() {}

        public bool invalid = false;

        public IEnumerable<SelectListItem> Options
        {
            get
            {
                var users_list = lab1.Entities.Users.load();
                var select_list = new List<SelectListItem>();
                select_list.AddRange(users_list.Select(s => new SelectListItem(s, s)));
                return select_list;
            }
        }
    }
}
