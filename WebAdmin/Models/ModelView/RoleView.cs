using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models.ModelView
{
    public class RoleView
    {
        public List<Permisstion> Permisstion { get; set; } = new List<Permisstion>();
        public List<Role> Role { get; set; } = new List<Role>();
    }
}