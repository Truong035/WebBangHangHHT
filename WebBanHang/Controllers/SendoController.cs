using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebBanHang.Models.Emtity;

namespace WebBanHang.Controllers
{
    public class Login
    {
        public string shop_key { get; set; } = "787462a7fe32467f86c90271f72a1256";
        public string secret_key { get; set; } = "0c78afe4f92a40f583ece5f227854799";
    }
    public class fee
    {
        public string pick_province { get; set; }
        public string pick_district { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string address { get; set; }
        public string transport { get; set; }
        public string deliver_option { get; set; }
        public string pick_ward { get; set; }
        public string pick_street { get; set; }

        public string pick_address { get; set; }
        public string ward { get; set; }


        public int? width { get; set; }
        public int? value { get; set; }

        public List<int> tags { get; set; } = new List<int>();

    }
    public class SendoController : Controller
    {
        WebBanHangDB db = new WebBanHangDB();
        // GET: Sendo
        public ActionResult Index()
        {
            return View();
        }



        public async Task<JsonResult> region(string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync("https://open.sendo.vn/api/address/region");
            string a = await response.Content.ReadAsStringAsync();
            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> district(int id, string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync("https://open.sendo.vn/api/address/district?regionId=" + id);
            string a = await response.Content.ReadAsStringAsync();
            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ward(int id, string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync("https://open.sendo.vn/api/address/ward?districtId=" + id);
            string a = await response.Content.ReadAsStringAsync();
            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> Login()
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(new Login());
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("https://open.sendo.vn/login", httpContent);
            string a = await response.Content.ReadAsStringAsync();
            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> Free(fee data)
        {
            var httpClient = new HttpClient();


            var Custormer = (Custormer)Session["Login"];

            var Carts = db.Carts.Where(x => x.uid.Equals(Custormer.uid)).ToList();

            foreach (var item in Carts)
            {
                data.width += (int)(0.01 * 1000) * item.quantity;
            }
            var json = JsonConvert.SerializeObject(data);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("Token", "891732705c99a1458BC0cc06732B2F2B77437363");
            HttpResponseMessage response = await httpClient.PostAsync("https://services.giaohangtietkiem.vn/services/shipment/fee", httpContent);
            string a = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);

            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
        }
    }
}