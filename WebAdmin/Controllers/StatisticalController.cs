using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;
using WebAdmin.Controllers;

namespace WebBanThuoc.Controllers
{
    public class StatisticalController : BaseController
    {
        class thongke
        {
            public string Thang;
            public decimal doanh;
            public decimal loi;
            public string benefit;
            public string revenue;

        }

        public ActionResult StatisticalProduct()
        {
            WebBanHangDB db = new WebBanHangDB();
            var order = db.VoucherOrders.Where(x => x.delete != true).ToList();
            return View();
        }

        public JsonResult statisticalProducts(string ngaybd, string ngayKT)
        {

            DateTime fromdate = DateTime.Now.AddMonths(-4);
            DateTime todate = DateTime.Now.AddDays(1);
            if (ngaybd != null && ngayKT != null && ngayKT.Length > 0 && ngaybd.Length > 0)
            {
                fromdate = DateTime.Parse(ngaybd);
                //  Session["ngaykt="] = DateTime.Parse(ngayKT);
                 todate = DateTime.Parse(ngayKT);
                
            }

            WebBanHangDB db = new WebBanHangDB();
            todate = todate.AddDays(1);
            var voucherOrder = db.VoucherOrders.Where(x => x.status > 1 && x.status != 6 && x.delete != true && x.createdate != null && x.createdate.Value >= fromdate && x.createdate.Value <todate).ToList();
            var product = db.Products.ToList();
            var voucherOrderDetail = db.VoucherOrderDetails.ToList();
            var productDetail = (from o in voucherOrder
                                 join od in voucherOrderDetail on o.id equals od.voucherId
                                 join p in product on od.product_id equals p.Id

                                 select new
                                 {
                                     p.Id,
                                     Name = p.name,
                                     grossAmount = od.grossAmount,
                                     discountAmount=   od.discountAmount,
                                     netAmount=(od.grossAmount - od.discountAmount)

                                 }
                                 ).ToList();
            var group = (from p in productDetail
                       group p by new { p.Name, p.Id }
                       into gr
                       select new
                       {
                           grossAmountValue= gr.Sum(x => x.grossAmount).Value,
                         name1= gr.Key.Name,
                           name = (gr.Key.Name.Length>20 ?gr.Key.Name.Substring(0,19): gr.Key.Name),
                           grossAmount = gr.Sum(x => x.grossAmount).Value.ToString("N0") + "VNĐ ",
                           discountAmount = gr.Sum(x => x.discountAmount).Value.ToString("N0") +"VNĐ ",
                             netAmount = gr.Sum(x => x.netAmount).Value.ToString("N0") + "VNĐ ",
                       }
                      ).OrderByDescending(x=>x.grossAmount).ToList();
            List<string> productAsc = new List<string>();
            List<long> productAscValue = new List<long>();

            List<string> productDesc = new List<string>();
            List<long> productDescValue = new List<long>();

            productAsc = group.OrderBy(x=>x.grossAmount).Select(x => x.name).Take(6).ToList();
            productAscValue = group.OrderBy(x => x.grossAmount).Select(x => ((long)x.grossAmountValue)).Take(6).ToList();

            productDesc = group.OrderByDescending(x => x.grossAmount).Select(x => x.name).Take(6).ToList();
            productDescValue = group.OrderByDescending(x => x.grossAmount).Select(x => ((long)x.grossAmountValue)).Take(6).ToList();


            return Json(new { productDesc , productDescValue, productAscValue, productAsc ,data= group.OrderBy(x=>x.grossAmount) }, JsonRequestBehavior.AllowGet);
        }

            public ActionResult StatisticalOrder()
        {
            WebBanHangDB db = new WebBanHangDB();
            var order = db.VoucherOrders.Where(x => x.delete != true).ToList();
            return View(order);
        }
        [HttpPost]
            public ActionResult StatisticalOrder(FormCollection collection)
        {
            string NBB= collection["NBD"];
            string NKT = collection["NKT"];
            int stust = 0;
        
            DateTime fromDate = DateTime.Now.AddYears(-1);
            DateTime ToDate = DateTime.Now;
            if (NBB.Length>0 && NKT.Length>0)
            {
                fromDate = DateTime.Parse(NBB);
                //  Session["ngaykt="] = DateTime.Parse(ngayKT);
                 ToDate = DateTime.Parse(NBB);
                ToDate= ToDate.AddDays(1);
            }

            WebBanHangDB db = new WebBanHangDB();
            var order = db.VoucherOrders.Where(x => x.delete != true  ).ToList();
            order = db.VoucherOrders.Where(x => x.status > 1 && x.status != 6 && x.delete != true && x.createdate != null && x.createdate >= fromDate && x.createdate <= ToDate).OrderByDescending(x => x.createdate).ToList();
            try
            {
                stust = int.Parse(collection["status"]);
                order = db.VoucherOrders.Where(x => x.status == stust && x.delete != true && x.createdate != null && x.createdate >= fromDate && x.createdate <= ToDate).OrderByDescending(x => x.createdate).ToList();
            }
            catch { }
            return View("StatisticalOrder",order);
        }


        public JsonResult ThongkeTheoThang(string ngaybd, string ngayKT)
        {

            WebBanHangDB db = new WebBanHangDB();
            var voucherOrder = db.VoucherOrders.Where(x => x.status > 1 && x.status != 6 && x.delete != true && x.createdate != null).ToList();
            //    int arr =[];
            DateTime dateTime = DateTime.Now;
            if (ngaybd != null && ngayKT != null && ngayKT.Length>0 && ngaybd.Length>0) {
                 dateTime = DateTime.Parse(ngaybd);
                //  Session["ngaykt="] = DateTime.Parse(ngayKT);
                DateTime dateTime1 = DateTime.Parse(ngayKT);
                voucherOrder = db.VoucherOrders.Where(x => x.status > 1 && x.status != 6 && x.delete != true && x.createdate != null && x.createdate >= dateTime && x.createdate <= dateTime1).OrderBy(x => x.createdate).ToList();
            }
            else
            {
                 voucherOrder = db.VoucherOrders.Where(x => x.status > 1 && x.status != 6 && x.delete != true && x.createdate != null && x.createdate.Value.Year == dateTime.Year).ToList();
            }
       
            var voucherOrders = (from o in voucherOrder
                                 select new
                                 {
                                     Month = "Tháng " + o.createdate.Value.Month+ " Năm " + o.createdate.Value.Year,
                                     o.grossAmount,
                                     o.discountAmount
                                 }
                               ).ToList();
            var arr = (from p in voucherOrders
                        group p by new { p.Month }
                       into gr
                        select new thongke()
                        {
                            Thang = gr.Key.Month ,
                            doanh = gr.Sum(x => x.grossAmount).Value,
                            loi= gr.Sum(x => x.grossAmount).Value- gr.Sum(x => x.discountAmount).Value,
                            revenue= gr.Sum(x => x.grossAmount).Value.ToString("N0"),
                            benefit = (gr.Sum(x => x.grossAmount).Value - gr.Sum(x => x.discountAmount).Value).ToString("N0")
                        }
                      ).ToList();

            //Session["ngaykt="] = DateTime.Now;
         
            //Session["ngaybd="] = dateTime.AddDays(-n);
          

            return Json(new
            {
                arr,

            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ThongkeTheonngay(string ngaybd, string ngayKT)
        {

            //    int arr =[];
            DateTime dateTime = DateTime.Now.AddMonths(-1);
            DateTime dateTime1 = DateTime.Now;
 
            if (ngaybd != null && ngayKT != null && ngayKT.Length > 0 && ngaybd.Length > 0)
            {
                dateTime = DateTime.Parse(ngaybd);
                //  Session["ngaykt="] = DateTime.Parse(ngayKT);
                 dateTime1 = DateTime.Parse(ngayKT);
                
            }
          //  Session["ngaybd="] = dateTime;
            //   thongke[] arr = new thongke[n];
            WebBanHangDB db = new WebBanHangDB();
            // List<OnlineShop.Areas.Admin.Models.ThongKeDanhMuc> thongKeDanhMucs = new List<Models.ThongKeDanhMuc>();
            //    DateTime date = dateTime.AddDays(-n);
            List<thongke> arr = new List<thongke>();
            List<double> arr1 = new List<double>();

            var order = db.VoucherOrders.Where(x => x.status > 1 && x.status != 6 && x.delete != true && x.createdate!=null   && x.createdate.Value.Day >= dateTime.Day && x.createdate.Value.Month >= dateTime.Month
            && x.createdate.Value.Year >= dateTime.Year && x.createdate.Value.Year <= dateTime1.Year
            && x.createdate.Value.Month <= dateTime1.Month && x.createdate.Value.Day <= dateTime1.Day
            ).OrderByDescending(x => x.createdate).ToList();

            //  arr[i - 1] = date.Day + "/" + date.Month+"/"+date.Year;
            var voucherOrders = (from o in order
                                 select new
                                 {
                                     Date = o.createdate.Value.ToShortDateString(),
                                     o.grossAmount,
                                     o.discountAmount
                                 }
                             ).ToList();
             arr = (from p in voucherOrders
                       group p by new { p.Date }
                       into gr
                       select new thongke()
                       {
                           Thang = "" + gr.Key.Date.ToString(),
                           doanh = gr.Sum(x => x.grossAmount).Value,
                           loi = gr.Sum(x => x.grossAmount).Value - gr.Sum(x => x.discountAmount).Value,
                           revenue = gr.Sum(x => x.grossAmount).Value.ToString("N0"),
                           benefit = (gr.Sum(x => x.grossAmount).Value - gr.Sum(x => x.discountAmount).Value).ToString("N0")
                       }
                      ).ToList();
         
            return Json(new
            {
                arr,
                arr1
            });
        }

        // GET: Statistical
        public ActionResult Index()
        {
            return View();
        }
    }
}