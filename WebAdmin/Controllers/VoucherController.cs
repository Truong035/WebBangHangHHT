using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;

namespace WebAdmin.Controllers
{
    public class VoucherController : BaseController
    {
        private WebBanHangDB db = new WebBanHangDB();
        // GET: Voucher
        public ActionResult Index()
        {

            return View(db.Vouchers.Where(x=>x.Delete!=true).ToList().Distinct());
        }
        // GET: Voucher
       [HttpGet]
        public ActionResult Create()
        {
          

                return View();
        }

     
        public ActionResult Delete(long id )
        {
            var voucher = db.Vouchers.Where(x => x.Id == id).FirstOrDefault();
            if (voucher == null)
            {

                return View("Error");
            }
            else
            {
                voucher.Delete = true;
                voucher.voucher_code = "";
                db.SaveChanges();
                return RedirectToAction("Index");

            }
           
        }
        [HttpPost]
        public ActionResult Create(Voucher voucher ,FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var IdProducts = collection["IdProducts"];
                string[] IdProduct = IdProducts.Split(',');
                if (db.Vouchers.Where(x => x.voucher_code.Equals(voucher.voucher_code)).FirstOrDefault() != null)
                {
                    ModelState.AddModelError("","Mã này đã đc sử dụng");

                    return View(voucher);
                }

                if (voucher.start_time == voucher.end_time || voucher.start_time > voucher.end_time)
                {
                    ModelState.AddModelError("", "Ngày áp dụng không hợp lệ");

                    return View(voucher);
                }

                Voucher voucher1 = new Voucher()
                {
                    Delete = false,
                    end_time = voucher.end_time,
                    start_time = voucher.start_time,
                    discount_value = voucher.discount_value,
                    voucher_code = voucher.voucher_code
                    
                };
            db.Vouchers.Add(voucher1);

            db.SaveChanges();

                foreach (var item in IdProduct)
                {
                    VoucherDetail detail = new VoucherDetail()
                    {
                        IdProduct = long.Parse(item),
                        IdVoucher = voucher1.Id,
                        
                    };
                    db.VoucherDetails.Add(detail);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
                return View(voucher);
        }

    }
}