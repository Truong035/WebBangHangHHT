using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models.ModelView
{
    public class UserView
    {
        public List<User> User { get; set; } = new List<User>();
        public List<Role> Role { get; set; } = new List<Role>();
    }
}