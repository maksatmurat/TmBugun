using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;
using PagedList.Mvc;
using PagedList;
using System.Net;

namespace TmBugun.Controllers
{
    public class SportlarsController : Controller
    {
        TmBugunDB db = new TmBugunDB();

        #region Sport ana sayfasi
        public ActionResult SportAnaSayfa()
        {
            var sporthaber = db.Sport.ToList();
            return View(sporthaber);
        }
        #endregion

        #region sport kategorileri
        public ActionResult AnaSayfaKategoriler()
        {
            var SportKategoriler = db.SportKategorileri.Take(8).ToList();
            return PartialView(SportKategoriler);
        }
        #endregion

        #region 1.kisim
        public ActionResult SportAnaSayfaSec1()
        {
            var tekli = db.Sport.Where(x => x.SportBlogTipId == 1).Take(1).ToList();
            return PartialView(tekli);
        }

        #endregion

        #region 1.1 kisim
        public ActionResult SportAnaSayfaSec1IkinciKisim()
        {
            var tekli = db.Sport.Where(x => x.SportBlogTipId == 1).Take(1).OrderBy(x => x.Id);
            return PartialView(tekli);
        }
        #endregion

        #region 2.kisim
        public ActionResult SportAnaSayfaSec2()
        {
            var onaltili = db.Sport.Where(x => x.SportBlogTipId == 4).Take(12).ToList();
            return PartialView(onaltili);
        }
        #endregion 

        #region 3.kisim
        public ActionResult SportAnaSayfaSec3()
        {
            var dortlu = db.Sport.Where(x => x.SportBlogTipId == 4).Take(3).ToList();
            return PartialView(dortlu);
        }
        #endregion

        #region 4.kisim
        public ActionResult SportAnaSayfaSec4()
        {
            var tekli = db.Sport.Where(x => x.SportBlogTipId == 1).Take(1).ToList();
            return PartialView(tekli);
        }
        #endregion

        #region 4.2 kisim
        public ActionResult SportAnaSayfaSec4IkinciKisim()
        {
            var uclu = db.Sport.Where(x => x.SportBlogTipId == 3).Take(6).ToList();
            return PartialView(uclu);
        }
        #endregion

        #region sport haber detayi
        public ActionResult sportdetay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewData["KullaniciID"] = Session["NormalID"];
            Sport spor = db.Sport.Find(id);
            if (spor == null)
            {
                return HttpNotFound();
            }
            return View(spor);
        }
        #endregion

        #region kategoriye bagli haberler
        public ActionResult kategoriSport(int id, int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            string KategoriAd = db.Sport.Where(x => x.SportkategoriId == id).FirstOrDefault()?.SportKategorileri.SportKategori;
            ViewData["kategori"] = KategoriAd;
            var katesport = db.Sport.Where(x => x.SportkategoriId == id).ToList().ToPagedList<Sport>(_sayfaNo, 12);
            return View(katesport);
        }
        #endregion

        #region Tum Sport Haberler
        public ActionResult TumSportHaberler(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var sport = db.Sport.OrderByDescending(x => x.SportTarih).ToList().ToPagedList<Sport>(_sayfaNo, 12);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Sportlars/SportListesi.cshtml", sport);
            }
            return View(sport);
        }
        #endregion

        #region Yorum Yapma islemi
        public JsonResult YorumYap1(string yorum, int kullaniciID, int SportId)
        {
            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorumlar.Add(new Yorumlar { SportID = SportId, KullaniciID = kullaniciID, Yorum = yorum, YorumTarih = DateTime.Now, HaberID = null, ResimID = null, YorumBegSayi = 0 });
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region sport haber begenme butonu
        public JsonResult BlBegen1(int? SportId)
        {
            Sport sport = db.Sport.Find(SportId);
            sport.SportBegenmeSayi = sport.SportBegenmeSayi + 1;
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Yorumu Begen butonu
        [HttpPost]
        public JsonResult YorumBegen(int? yorumid)
        {
            Yorumlar yorumlar = db.Yorumlar.Find(yorumid);
            yorumlar.YorumBegSayi = yorumlar.YorumBegSayi + 1;
            db.SaveChanges();
            return Json(yorumlar, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Yorumlar Gosterildigi kisim
        public ActionResult SportHabereBagliYorumlar(int? id)
        {
            var yorumlar = db.Yorumlar.Where(x => x.SportID == id).OrderByDescending(x => x.YorumTarih).ToList();
            return PartialView(yorumlar);
        }
        #endregion

        #region Sport Haber izlenmeyi arttir
        public ActionResult IzlemeArttir(int haberid)
        {
            var haber = db.Sport.Where(m => m.Id == haberid).SingleOrDefault();
            haber.SportIzlenmeSayi = haber.SportIzlenmeSayi + 1;
            db.SaveChanges();
            return View();
        }
        #endregion






        public ActionResult CL()
        {
            return PartialView();
        }
        public ActionResult son10sport()
        {
            var son10 = db.Sport.OrderByDescending(x=>x.SportTarih).Take(10);
            return PartialView(son10);
        }
        public ActionResult paylasbutton()
        {
            return PartialView();
        }
     
    }
}