using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TmBugun.Models;

namespace TmBugun.Areas.Admin.Controllers
{
    public class KayitIslemleriController : Controller
    {
        // GET: Admin/KayitIslemleri
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Models.AdminBilgileri adminBilgileri)
        {
            using (TmBugunDB db = new TmBugunDB())
            {
                var admindetay = db.AdminBilgileri.Where(x => x.AdminAd == adminBilgileri.AdminAd && x.Adminsifre == adminBilgileri.Adminsifre).FirstOrDefault();
                if (admindetay != null)
                {
                    Session["AdminID"] = admindetay.Id;
                    Session["AdminAd"] = admindetay.AdminAd + " ";
                    return RedirectToAction("Index", "AnaSayfa", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Hatala Sifre veya Email");
                    return View("Login", adminBilgileri);
                }
            }
        }
        public ActionResult Logout()
        {
            Session["AdminID"] = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}