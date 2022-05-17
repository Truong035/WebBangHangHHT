using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models.ModelView
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}