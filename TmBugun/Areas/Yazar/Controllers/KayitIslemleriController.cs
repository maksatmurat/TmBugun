using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TmBugun.Models;

namespace TmBugun.Areas.Yazar.Controllers
{
    public class KayitIslemleriController : Controller
    {
        // GET: Yazar/KayitIslemleri
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Models.Yayimci yayimci)
        {
            using (TmBugunDB db = new TmBugunDB())
            {
                var  yazardetay = db.Yayimci.Where(x => x.Yayimci_eposta == yayimci.Yayimci_eposta && x.Yayimci_sifre == yayimci.Yayimci_sifre).FirstOrDefault();
                if (yazardetay != null)
                {
                    Session["YazarID"] = yazardetay.Id;
                    Session["YazarAd"] = yazardetay.Yayimci_ad + " " + yazardetay.Yayimci_soyad;
                    Session["YazarResim"] = yazardetay.YayimciResim;
                    return RedirectToAction("Index", "AnaSayfa", new { area = "Yazar" });
                }
                else
                {
                    ModelState.AddModelError("", "Hatala Sifre veya Email");
                    return View("Login", yayimci);
                }
            }
        }
        public ActionResult Logout()
        {
            Session["YazarID"] = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}