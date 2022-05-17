using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Models;

namespace WebAdmin.Controllers
{
    public class CategoryController : Controller
    {
        WebBanHangDB db = new WebBanHangDB();
        public ActionResult Index()
        {

            
            var Categories = db.Categories.Where(x => (x.delete == false || x.delete == null)).ToList();
            return View(Categories);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {

            var id = collection["idCategory"];
            var Url = collection["Url"];
            if (id.Length > 0)
            {

                Categorie categoryupdate = db.Categories.Find(int.Parse(id));
                categoryupdate.display_name = collection["Name"];
                categoryupdate.image = Url;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
              
                Categorie categorynew = new Categorie();
                categorynew.Id = db.Categories.ToList().Last().Id + 1;
                categorynew.display_name = collection["Name"];
                categorynew.image = Url;
                categorynew.delete = false;
                db.Categories.Add(categorynew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return View("Error");
            }
        
            Categorie category = db.Categories.Find(id);
            category.delete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}