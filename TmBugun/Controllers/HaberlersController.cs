using System.Linq;
using System.Web.Mvc;
using TmBugun.Models;
using PagedList;
using System.Net;
using PagedList.Mvc;
using PagedList;
using System;

namespace TmBugun.Controllers
{
    public class HaberlersController : Controller
    {
        TmBugunDB db = new TmBugunDB();

        #region Haberler Ana Sayfasi
        public ActionResult AnaSayfa(int Page = 1)
        {
            var haber = db.Haber.OrderByDescending(m => m.Id).ToPagedList(Page, 4);
            return View(haber);
        }
        #endregion

        #region Kisim1
        public ActionResult AnaSayfaSec1()
        {
            var tekli = db.Haber.Where(x => x.HaberBlogTipId == 1).Take(1).ToList();
            return PartialView(tekli);
        }
        #endregion

        #region kisim1.2
        public ActionResult AnaSayfaSec1IkinciKisim()
        {
            var ikili = db.Haber.Where(x => x.HaberBlogTipId == 2).Take(2).ToList();
            return PartialView(ikili);
        }
        #endregion

        #region kisim2
        public ActionResult AnaSayfaSec2()
        {
            var uclu = db.Haber.Where(x => x.HaberBlogTipId == 3).Take(3).ToList();
            return PartialView(uclu);
        }
        #endregion

        #region kisim2.2
        public ActionResult AnaSayfaSec2IkinciKisim()
        {
            var dortlu = db.Haber.Where(x => x.HaberBlogTipId != 1).Take(4).ToList();
            return PartialView(dortlu);
        }
        #endregion

        #region Tum Haberler
        public ActionResult TumHaberler(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var haber = db.Haber.OrderByDescending(x => x.HaberTarih).ToList().ToPagedList<Haber>(_sayfaNo, 12);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Haberlers/HaberListesi.cshtml", haber);
            }
            return View(haber);
        }
        #endregion

        #region kisim3
        public ActionResult AnaSayfaSec3()
        {
            var encok = db.Haber.OrderByDescending(x => x.HaberIzlenmeSayi).Take(1).ToList();
            return PartialView(encok);
        }

        #endregion

        #region Kisim3.2
        public ActionResult AnaSayfaSec3IkinciKisim()
        {
            var altili = db.Haber.Where(x => x.HaberBlogTipId != 1).Take(6).ToList();
            return PartialView(altili);
        }
        #endregion

        #region Kisim4
        public ActionResult AnaSayfaSec4()
        {
            var altili = db.Haber.OrderByDescending(x => x.HaberBegenmeSayi).Take(6).ToList();
            return PartialView(altili);
        }
        #endregion

        #region Kisim5
        public ActionResult AnaSayfaSec5()
        {
            var Kategori1EnFazla = db.Haber.Where(x => x.HaberKategorileri.Id == 1).OrderByDescending(x => x.HaberIzlenmeSayi).Take(1).ToList();
            ViewData["Kategori1EnFazla"] = Kategori1EnFazla;
            var Kategori1 = db.Haber.Where(x => x.HaberKategorileri.Id == 1).OrderByDescending(x => x.HaberIzlenmeSayi).Take(3).ToList();
            ViewData["kategori1"] = Kategori1;

            var Kategori2EnFazla = db.Haber.Where(x => x.HaberKategorileri.Id == 2).OrderByDescending(x => x.HaberIzlenmeSayi).Take(1).ToList();
            ViewData["Kategori2EnFazla"] = Kategori2EnFazla;
            var Kategori2 = db.Haber.Where(x => x.HaberKategorileri.Id == 2).OrderByDescending(x => x.HaberIzlenmeSayi).Take(3).ToList();
            ViewData["kategori2"] = Kategori2;

            var Kategori3EnFazla = db.Haber.Where(x => x.HaberKategorileri.Id == 3).OrderByDescending(x => x.HaberIzlenmeSayi).Take(1).ToList();
            ViewData["Kategori3EnFazla"] = Kategori3EnFazla;
            var Kategori3 = db.Haber.Where(x => x.HaberKategorileri.Id == 3).OrderByDescending(x => x.HaberIzlenmeSayi).Take(3).ToList();
            ViewData["kategori3"] = Kategori3;
            return PartialView();
        }

        #endregion

        #region  Haber Ana Sayfa Kategorileri 
        public ActionResult AnaSayfaKategoriler()
        {
            return PartialView("AnaSayfaKategoriler", db.HaberKategorileri.Take(8).ToList());
        }
        #endregion

        #region Kategorilere Gore Haberler
        public ActionResult KategoriHaber(int? id,int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var baglihaberler = db.Haber.Where(x => x.HaberkategoriId == id).ToList().ToPagedList<Haber>(_sayfaNo, 12);
            string KategoriAd = db.Haber.Where(x => x.HaberkategoriId == id).FirstOrDefault()?.HaberKategorileri.HaberKategori;
            ViewData["kategori"] = KategoriAd;
            return View(baglihaberler);
        }

        #endregion

        #region En cok Okunan Haber
        public ActionResult EnCok()
        {
            var encok = db.Haber.OrderBy(x => x.HaberIzlenmeSayi).Take(3);
            return PartialView(encok.ToList());
        }
        #endregion

        #region En son eklenen 10 haber
        public ActionResult EnSon()
        {
            var enson = db.Haber.OrderByDescending(x => x.HaberTarih).Take(10);
            return PartialView(enson.ToList());
        }
        #endregion

        #region Haber Detay Sayfasi
        public ActionResult HaberDetay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Haber haber = db.Haber.Find(id);
            if (haber == null)
            {
                return HttpNotFound();
            }
            return View(haber);
        }

        #endregion

        #region Yorum Yapma actioni
        public JsonResult YorumYap(string yorum,int kullaniciID, int Blogid)
        {
            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorumlar.Add(new Yorumlar { HaberID = Blogid, KullaniciID = kullaniciID, Yorum = yorum, YorumTarih = DateTime.Now, SportID = null,ResimID=null,YorumBegSayi=0});
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Haber Begeni Butonu
        public JsonResult BlBegen(int? haberid)
        {
            Haber haber = db.Haber.Find(haberid);
            haber.HaberBegenmeSayi = haber.HaberBegenmeSayi + 1;
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Yorum Begeni Butonu
        [HttpPost]
        public JsonResult YorumBegen(int? yorumid)
        {
            Yorumlar yorumlar = db.Yorumlar.Find(yorumid);
            yorumlar.YorumBegSayi = yorumlar.YorumBegSayi + 1;
            db.SaveChanges();
            return Json(yorumlar, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region yorumlar gosterildigi kisim
        public ActionResult HabereBagliYorumlar(int? id)
        {
            var yorumlar = db.Yorumlar.Where(x => x.HaberID == id).OrderByDescending(x => x.YorumTarih).ToList();
            return PartialView(yorumlar);
        }
        #endregion

        #region Izlenme arttir
        public ActionResult IzlemeArttir(int haberid)
        {
            var haber = db.Haber.Where(m => m.Id == haberid).SingleOrDefault();
            haber.HaberIzlenmeSayi = haber.HaberIzlenmeSayi + 1;
            db.SaveChanges();
            return View();
        }
        #endregion
    }
}