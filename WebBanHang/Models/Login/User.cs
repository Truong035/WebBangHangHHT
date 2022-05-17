using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models.Login
{
    public class User
    {
        [Required(ErrorMessage = "Vui lòng nhập vào ô trống")]
        public string UseName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào ô trống")]
        public string Pass { get; set; }

    }
}