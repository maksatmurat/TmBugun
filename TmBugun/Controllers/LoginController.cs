using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using TmBugun.Models;

namespace TmBugun.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Models.NormalKullanici normal)
        {
            using (TmBugunDB db = new TmBugunDB())
            {
                var normaldetay = db.NormalKullanici.Where(x => x.NormalKullanici_eposta==normal.NormalKullanici_eposta && x.sifre == normal.sifre).FirstOrDefault();
                if (normaldetay != null)
                {
                    Session["NormalID"] = normaldetay.Id;
                    Session["NormalAdi"] = normaldetay.KullaniciAd;
                    Session["NormalEmail"] = normaldetay.NormalKullanici_eposta;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Hatala Sifre veya Email");
                    ViewBag["Hata"] = "Hatala Sifre veya Email";
                    return View("Login", normal);
                }
            }
        }
        [HttpPost]
        public ActionResult SignIn(Models.NormalKullanici normal)
        {
                using (TmBugunDB db = new TmBugunDB())
                {
                    bool varmi = db.NormalKullanici.Any(x => x.NormalKullanici_eposta==normal.NormalKullanici_eposta);
                    if (varmi == true)
                    {
                        ModelState.AddModelError("", "Bu Gmail Kayitli Giris Yapiniz Bir Kullanici Mevcut");
                        return View("Login", normal);
                    }
                    else{
                        Session["NormalID"] = normal.Id;
                        Session["NormalAdi"] = normal.KullaniciAd;
                        Session["NormalEmail"] = normal.NormalKullanici_eposta;
                        db.NormalKullanici.Add(normal);
                        db.SaveChanges();
                        return RedirectToAction("Index","Home");
                    }
                }
            return View("Login", normal);
        }

        public ActionResult Logout()
        {
            Session["NormalID"] = null;
            Session["NormalAdi"] = null;
            Session["NormalEmail"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}