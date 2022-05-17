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
using WebAdmin.Models.SendoData;

namespace WebAdmin.Controllers
{
    public class SendoController : Controller
    {
        // GET: Sendo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
     
        public async Task<JsonResult> updateStatus(long? id, string token,int status,string cancel_order_reason)
        {
         
            var httpClient = new HttpClient();
            UpdateStatusOrder order = new UpdateStatusOrder()
            {
                order_status = status,
                order_number = id.Value.ToString(),
                cancel_order_reason=cancel_order_reason
            };
            var json = JsonConvert.SerializeObject(order);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.PutAsync("https://open.sendo.vn/api/partner/salesorder", httpContent);
            string a = await response.Content.ReadAsStringAsync();
            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Detail(long id, string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync("https://open.sendo.vn/api/partner/salesorder/" + id);
            string a = await response.Content.ReadAsStringAsync();
            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult detailOrder(long? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            return View();
        }

        public ActionResult ListOrder()
        {
            return View();
        }
        public async Task<JsonResult> ListOrders(string token)
        {
            var httpClient = new HttpClient();
            PageOrder order = new PageOrder()
            {
                order_date_from = DateTime.Now.AddMonths(-5),
                order_date_to = DateTime.Now,
                page_size = 0,
            };
            var json = JsonConvert.SerializeObject(order);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.PostAsync("https://open.sendo.vn/api/partner/salesorder/search", httpContent);
            var json_serializer = new JavaScriptSerializer();
            string a = await response.Content.ReadAsStringAsync();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);

        }

        public async Task<JsonResult> category(string id ,string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.GetAsync(" https://open.sendo.vn/api/partner/category/" + id);
            string a = await response.Content.ReadAsStringAsync();

            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
          

        }

        [ValidateInput(false)]
        public async Task<JsonResult> productCreate(ProductCreate product, string token)
        {
            var httpClient = new HttpClient();
            // product.promotion_from_date = DateTime.Now;
            // product.promotion_to_date = DateTime.Now.AddDays(15);
            product.avatar = new Avatar() { picture_url = product.pictures.ToList().FirstOrDefault().picture_url };
            var json = JsonConvert.SerializeObject(product);

            
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.PostAsync("https://open.sendo.vn/api/partner/product", httpContent);
            string a = await response.Content.ReadAsStringAsync();
            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
            // return "ssss";

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

        public async Task<JsonResult> Search(Page data,string token)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(data);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.PostAsync("https://open.sendo.vn/api/partner/product/search", httpContent);
            string a = await response.Content.ReadAsStringAsync();
            var json_serializer = new JavaScriptSerializer();
            object routes_list = json_serializer.DeserializeObject(a);
            return Json(new { routes_list }, JsonRequestBehavior.AllowGet);
      //      return routes_list;
        }
    }
}