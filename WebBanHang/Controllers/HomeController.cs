using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebBanHang.Models;
using WebBanHang.Models.Emtity;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
     private   WebBanHangDB DB = new WebBanHangDB();

        string serectkey = "sSFqWXSq4QXW9MSr5SDAaGNBjL7FCrIk";

        public JsonResult notifyurl()
        {
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ThanhToan(FormCollection collection)
        {
            var Custormer = (Custormer)Session["Login"];
          
            var Carts = DB.Carts.Where(x => x.uid.Equals(Custormer.uid)).ToList();
            var Name = collection["Name"];
            var Email = collection["Email"];
            var SDT = collection["SDT"];
            var region = collection["region"];
            var district = collection["district"];
            var ward = collection["ward"];
            var Adress = collection["Adress"];
            var Note = collection["Note"];
            var shipe = collection["ship"];
            var RadioPayment = collection["flexRadioPayment"];

            VoucherOrder voucherOrder = new VoucherOrder();
            voucherOrder.Name = Name;
            voucherOrder.email = Email;
            voucherOrder.province = region;
            voucherOrder.district = district;
            voucherOrder.ward = ward;
            voucherOrder.telephone = SDT;
            voucherOrder.adrees = Adress;
            voucherOrder.note = Note;
            voucherOrder.discountAmount = Carts.Sum(x => (x.Product?.discount!=null && x.Product.discount.Length>0? x.Product.price_before_discount-x.Product.price:0));
            voucherOrder.grossAmount = Carts.Sum(x => x.Product.price * x.quantity).Value;
            voucherOrder.uid = Custormer.uid;
            voucherOrder.delete = false;
            voucherOrder.paymentmethods = 0;
            voucherOrder.createdate = DateTime.Now;
            voucherOrder.status = 1;
            voucherOrder.shiper = 0;
            var payment = Carts.Sum(x => x.intomoney);
            if (shipe.Length > 0)
            {
                payment = decimal.Parse(shipe) + payment;
                voucherOrder.shiper = decimal.Parse(shipe);
            }
            if (RadioPayment != null && RadioPayment.Equals("momo"))
            {
                voucherOrder.paymentmethods = 1;
                string partnerCode = "MOMODDI520210624";
                string accessKey = "xc5ROj205YDvqrBn";
                string orderId = Guid.NewGuid().ToString();
                string requestId = Guid.NewGuid().ToString();
                string extraData = "";
                string orderInfo = "Tên Khách Hàng : " + Custormer.name;
                string amount = "" + payment;
                string redirectUrl = "https://localhost:44310/Home/returnUrl";
                string ipnUrl = "https://localhost:44310/notifyurl";
                string requestType = "captureWallet";
                //Before sign HMAC SHA256 signature
                string rawHash = "accessKey=" + accessKey +
                    "&amount=" + amount +
                    "&extraData=" + extraData +
                    "&ipnUrl=" + ipnUrl +
                    "&orderId=" + orderId +
                    "&orderInfo=" + orderInfo +
                    "&partnerCode=" + partnerCode +
                    "&redirectUrl=" + redirectUrl +
                    "&requestId=" + requestId +
                    "&requestType=" + requestType
                    ;


                MoMoSecurity crypto = new MoMoSecurity();
                //sign signature SHA256
                string signature = crypto.signSHA256(rawHash, serectkey);


                //build body json request
                JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "en" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }

            };
                var httpClient = new HttpClient();

                var httpContent = new StringContent(message.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", httpContent);
                string a = await response.Content.ReadAsStringAsync();
                JObject jmessage = JObject.Parse(a);
                Session["Order"] = voucherOrder;
                return Redirect(jmessage.GetValue("payUrl").ToString());

            }
            else
            {
                DB.VoucherOrders.Add(voucherOrder);
                DB.SaveChanges();

                foreach (var item in Carts)
                {
                    DB.VoucherOrderDetails.Add(new VoucherOrderDetail()
                    {
                        product_id = item.product_id,
                        quantity = item.quantity,
                        grossAmount = item.intomoney,
                        discountAmount = item.Product.price - item.intomoney,
                        voucherId = voucherOrder.id

                    });
                    DB.SaveChanges();

                }
                DB.Carts.RemoveRange(Carts);
                DB.SaveChanges();
            }

            ViewBag.Message = "Thanh toán thành công!";
            return View("returnUrl");
        }

        public ActionResult returnUrl()
        {
            try
            {
                string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
                param = Server.UrlDecode(param);
                MoMoSecurity crypto = new MoMoSecurity();
                //string serectkey = ConfigurationManager.AppSettings["serectkey"].ToString();
                string serectKey = serectkey.ToString();
                string signature = crypto.signSHA256(param, serectKey);
                //if (signature != Request["signature"].ToString())
                //{
                //    ViewBag.Message = "Thông tin không hợp lệ!";
                //    return View();
                //}
                if (Request.QueryString["message"].Equals("Successful."))
                {
                    ViewBag.Message = "Thanh toán thành công!";
                    var Custormer = (Custormer)Session["Login"];
                  
                    var Carts = DB.Carts.Where(x => x.uid.Equals(Custormer.uid)).ToList();
                    VoucherOrder voucherOrder = (VoucherOrder)Session["Order"];
                    voucherOrder.status = 2;
                    DB.VoucherOrders.Add(voucherOrder);
                    DB.SaveChanges();

                    foreach (var item in Carts)
                    {
                        DB.VoucherOrderDetails.Add(new VoucherOrderDetail()
                        {
                            product_id = item.product_id,
                            quantity = item.quantity,
                            grossAmount = item.Product.price.Value * item.quantity.Value,
                            discountAmount = (item.Product.discount != null && item.Product.discount.Length >0? (item.Product.price_before_discount.Value - item.Product.price.Value)* item.quantity.Value :
                            0
                            ),
                            //item.Product.discount_stock.Value * item.quantity.Value,
                            voucherId = voucherOrder.id

                        });
                        DB.SaveChanges();
                        var product = DB.Products.Find(item.id);
                        product.stock = product.stock - item.quantity;
                        DB.SaveChanges();
                    }

                    DB.Carts.RemoveRange(Carts);
                    DB.SaveChanges();
                    return View();
                }
                else
                {
                    ViewBag.Message = "Thanh toán thất bại!";
                }
                ViewBag.Message = "Thanh toán thất bại!";
                return View("ErollUrl");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
        public async Task<ActionResult> Index()
        {
            return View();
        }
        public ActionResult Home()
        {
        
            return PartialView();
        }
        public ActionResult UseInfor()
        {
            Custormer custormer = new Custormer();
            try
            {
                custormer = (Custormer)Session["Login"];
            }
            catch { }
            return PartialView(custormer);
        }

        public async Task<ActionResult> Detail(long id)
        {
          
            return View(DB.Products.Where(x=>x.Id==id).FirstOrDefault());

        }
        public ActionResult Cart()
        {
            try
            {
                var Custormer = (Custormer)Session["Login"];
       
                return PartialView(DB.Carts.Where(x => x.uid.Equals(Custormer.uid)).ToList());
            }
            catch { }
            return PartialView(new List<Cart>());
        }
        public ActionResult AddCart(int? id, int? quantity,long Option)
        {
            try
            {

                if (id == null)
                {
                    return View("Error");
                }
                if (quantity == null ||quantity==0)
                {
                    quantity = 1;
                }

           
                Product product = DB.Products.Where(x => x.Id == id).FirstOrDefault();
                Custormer custormer = (Custormer)Session["Login"];
                if (custormer == null)
                {
                    Session["Url"] = $"/Home/AddCart/{id.Value.ToString()}?quantity={quantity.Value.ToString()}&Option={Option.ToString()}";
                    return RedirectToAction("Login", "Account");
                }
                Cart cart = DB.Carts.Where(x => x.product_id == product.Id && x.uid.Trim().Equals(custormer.uid.Trim())).FirstOrDefault();

                if (cart != null)
                {
                    cart.IdOption = Option;
                    cart.quantity += quantity;
                    var price = product.price;
                    cart.uid = custormer.uid;
                    cart.intomoney = cart.quantity * (price);
                    DB.SaveChanges();
                }
                else
                {
                    cart = new Cart();
                    cart.IdOption = Option;
                    cart.quantity = quantity;
                    cart.product_id = id;
                    var price = product.price;
                    cart.intomoney = cart.quantity * (price);
                    cart.uid = custormer.uid;
                    DB.Carts.Add(cart);
                    DB.SaveChanges();
                }
                Session["Url"] = "";
                return RedirectToAction("Cart");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Account");
            }
        }
        /// <summary>
        /// Thanh toán 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListOrder()
        {
            try
            {
                var Custormer = (Custormer)Session["Login"]; 
                
                var Carts = DB.Carts.Where(x => x.uid.Equals(Custormer.uid)).ToList();
                ViewBag.Use = Custormer;
                return View(Carts);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public async Task<ActionResult> ListAll(long? id ,int? page, string sortOrder, string searchString)
        {
            if (id == null)
            {
                id=  DB.Categories.FirstOrDefault()?.Id;
                ViewBag.url = id;
                ViewBag.idCategory = id;
            }
            if (sortOrder == null)
            {

                sortOrder = "asc";
            }
            ViewBag.sortOrder = sortOrder;
            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;
            ViewBag.searchValue = searchString;
            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var Product = new List<Product>();
                //DB.Products.Where(x => x.category_id == id).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                Product = DB.Products.Where(s => s.name.Contains(searchString) || s.price.Value.ToString().Contains(searchString)).ToList();
            }
            else
            {

                Product =  DB.Products.Where(x => x.category_id == id).ToList()
                           ;
                ViewBag.url = id;
                ViewBag.idCategory =id;
            }
            if (sortOrder == "asc")
            {
                Product = (from l in Product.ToList()
                           select l).OrderBy(x => x.price).ToList();
            }
            else
            {
                Product = (from l in Product
                           select l).OrderByDescending(x => x.price).ToList();
            }
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 6;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            return View(Product.ToPagedList(pageNumber, pageSize));
         
        }

        //public async Task<ActionResult> Index()
        //{
        //    var httpClient = new HttpClient();
        //    try
        //    {
        //        WebBanHangDB web = new WebBanHangDB();
        //        var C = web.Categories.Where(x=>x.Id>10).ToList();

        //        foreach (var item in C)
        //        {

        //            HttpResponseMessage response = await httpClient.GetAsync("https://shopee.vn/api/v4/search/search_items?by=pop&limit=100&match_id=25211549&newest=0&order=desc&page_type=shop&scenario=PAGE_OTHERS&version=2&shop_categoryids=" + item.shop_category_id.ToString());
        //            string a = await response.Content.ReadAsStringAsync();

        //            dynamic objData = JsonConvert.DeserializeObject<dynamic>(a);
        //            objData = JsonConvert.DeserializeObject<dynamic>(objData["items"].ToString());


        //            foreach (var item1 in objData)
        //            {
        //                 response = await httpClient.GetAsync($"https://shopee.vn/api/v4/item/get?itemid="+item1.itemid+"&shopid=25211549");
        //                 a = await response.Content.ReadAsStringAsync();

        //                 objData = JsonConvert.DeserializeObject<dynamic>(a);
        //                objData = JsonConvert.DeserializeObject<dynamic>(objData["data"].ToString());
        //          //  Product categories = new Product();
        //            Product categories = new Product()
        //            {
        //                name = objData.name,
        //                description = objData.description,
        //                brand = objData.brand,
        //                liked_count = objData.liked_count,
        //                cmt_count = objData.cmt_count,
        //                historical_sold = objData.historical_sold,
        //                discount = objData.discount,

        //                price = objData.price,
        //                category_id = item.Id,


        //                price_before_discount = objData.price_before_discount,
        //                price_min_before_discount = objData.price_min_before_discount,
        //                image = objData.image,
        //                show_free_shipping = objData.show_free_shipping,
        //                size_chart = objData.size_chart,
        //                rating_star = objData.item_rating.rating_star,
        //                stock = objData.stock,
        //                discount_stock = objData.discount_stock,


        //                //discount= discount
        //            };

        //            web.Products.Add(categories);
        //            web.SaveChanges();
        //            int k = 0;
        //            var comments= JsonConvert.DeserializeObject<dynamic>(objData["item_rating"].ToString());
        //             comments = JsonConvert.DeserializeObject<dynamic>(comments["rating_count"].ToString());
        //            foreach (var item2 in comments)
        //            {
        //                if (k > 0)
        //                {
        //                    for (int j = 0; j < item2.Value; j++)
        //                    {
        //                        Comment comment = new Comment()
        //                        {
        //                            CreateDate = DateTime.Now,
        //                            UseName = TaoName(),
        //                            Start = k,
        //                            Comment1 = "",
        //                            idProduct = categories.Id,
        //                        };
        //                        web.Comments.Add(comment);
        //                        web.SaveChanges();
        //                    }
        //                }
        //                k++;
        //            }



        //            var tier_variations = JsonConvert.DeserializeObject<dynamic>(objData["tier_variations"].ToString());

        //            if (tier_variations.Count > 0)
        //            {

        //                foreach (var item2 in tier_variations[0]?.options)
        //                {
        //                    Option_Product option = new Option_Product()
        //                    {
        //                        IdProduct = categories.Id,
        //                        option = item2,

        //                    };
        //                    web.Option_Product.Add(option);
        //                    web.SaveChanges();
        //                }
        //            }

        //            foreach (var item2 in objData?.images)
        //            {
        //                Img option = new Img()
        //                {
        //                    IdProduct = categories.Id,
        //                    Url = "https://cf.shopee.vn/file/" + item2,

        //                };
        //                web.Imgs.Add(option);
        //                web.SaveChanges();
        //            }

        //            var shop_vouchers = JsonConvert.DeserializeObject<dynamic>(objData["shop_vouchers"].ToString());

        //            foreach (var item2 in shop_vouchers)
        //            {
        //                Voucher voucher = new Voucher() {
        //                voucher_code=item2.voucher_code,
        //                discount_value=item2.discount_value,
        //                start_time= DateTime.Now,
        //                end_time=DateTime.Now.AddMonths(2),
        //                icon_text=item2.icon_text,
        //                IdProduct=categories.Id,
        //                };

        //                web.Vouchers.Add(voucher);
        //                web.SaveChanges();
        //            }



        //            // await web.SaveChangesAsync();
        //        }
        //        }



        //    }
        //    catch (Exception e)
        //    {

        //        return null;
        //    }



        //    return View();
        //}

        public string TaoName()
        {
            List<string> Names = new List<string>()
            {
                " Phạm Trọng Trường","Nguyễn Hoàng Vương","Hoàng Huy Tuấn","Nguyễn Mai Chí Trung","Phạm Thành Hậu","Đỗ Quốc Tuấn","Trần Thị Dung","Đỗ Hoàng Gia","Đinh Xuân Sơn","Trần Tiến Đạt"

            };
            return Names[new Random().Next(0, Names.Count)];
        }


            //public async Task<ActionResult> Index()
            //{
            //    var httpClient = new HttpClient();
            //    try {
            //        WebBanHangDB web = new WebBanHangDB();
            //        var C = web.Categories.ToList();



            //        HttpResponseMessage response = await httpClient.GetAsync("https://shopee.vn/api/v4/shop/get_categories?limit=20&offset=0&shopid=25211549");
            //        string a = await response.Content.ReadAsStringAsync();

            //        dynamic objData = JsonConvert.DeserializeObject<dynamic>(a);
            //        objData = JsonConvert.DeserializeObject<dynamic>(objData["data"].ToString());
            //        objData = JsonConvert.DeserializeObject<dynamic>(objData["shop_categories"].ToString());

            //        foreach (var item in objData)
            //        {
            //            Categorie categories = new Categorie()
            //            {
            //                shop_category_id = item.shop_category_id,
            //                display_name=item.display_name,
            //                image=item.image,
            //                total=item.total
            //            };

            //            web.Categories.Add(categories);
            //            await web.SaveChangesAsync();
            //        }


            //    }
            //    catch(Exception e)
            //    {

            //        return null;
            //    }



            //    return View();
            //}

            public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetAllOrder(int? id)
        {
            try
            {
                if (id == null) id = 1;

                ViewBag.Status = id;
                var Custormer = (Custormer)Session["Login"];

                var Carts = DB.VoucherOrders.Where(x => x.uid.Equals(Custormer.uid) && x.status == (id ?? 1) && x.delete != null).ToList();
                ViewBag.Use = Custormer;
                return View(Carts);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public ActionResult DetailOrder(FormCollection collection)
        {
            try
            {
                var value = collection["flexRadioDefault"];
                var id = collection["idorder"];
                long Id = long.Parse(id);
                VoucherOrder VoucherOrder = DB.VoucherOrders.Find(Id);
                VoucherOrder.statusCancel = int.Parse(value);
                VoucherOrder.status = 6;
                DB.SaveChanges();
                return Redirect("/Home/GetAllOrder/6");
            }
            catch { return View("Error"); }
        }
        public ActionResult DetailOrder(long? id)
        {
            try
            {

                if (id == null)
                {
                    return View("Error");
                }
          
                VoucherOrder VoucherOrder = DB.VoucherOrders.Where(x => x.id == id).FirstOrDefault();

                return View(VoucherOrder);
            }
            catch { return View("Error"); }
        }
        public ActionResult DeleteCart(long? id)
        {
            try
            {

                if (id == null)
                {
                    return View("Error");
                }  
                Cart cart = DB.Carts.Find(id);
                DB.Carts.Remove(cart);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View("Error"); }
        }

    }
}