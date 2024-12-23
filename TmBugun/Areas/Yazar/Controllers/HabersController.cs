using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;
using PagedList.Mvc;
using PagedList;


namespace TmBugun.Areas.Yazar.Controllers
{
    public class HabersController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Yazar/Habers
        public ActionResult Index(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            int yazarid =Convert.ToInt32(Session["YazarID"]);
            var haber = db.Haber.Include(h => h.HaberBlogTip).Include(h => h.HaberKategorileri).Include(h => h.Yayimci).Where(x=>x.YayimciId==yazarid).OrderByDescending(x => x.HaberTarih).ToList().ToPagedList<Haber>(_sayfaNo, 9);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Yazar/Views/Habers/HaberListesi.cshtml", haber);
            }
            return View(haber);
        }

        // GET: Yazar/Habers/Details/5
        public ActionResult Details(int? id)
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

            ViewData["haberId"] = haber.Id;
            return View(haber);
        }

        // GET: Yazar/Habers/Create
        public ActionResult Create()
        {
            ViewBag.HaberBlogTipId = new SelectList(db.HaberBlogTip, "Id", "HaberBlogTipi");
            ViewBag.HaberkategoriId = new SelectList(db.HaberKategorileri, "Id", "HaberKategori");
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad");
            return View();
        }

        // POST: Yazar/Habers/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HaberEkle([Bind(Include = "Id,HaberBaslik,HaberIcerik,HaberTarih,HaberIzlenmeSayi,HaberBegenmeSayi,HaberkategoriId,HaberBlogTipId,HaberResim,YayimciId,HaberBaslikEN,HaberIcerikEN,HaberBaslikRU,HaberIcerikRU,HaberBaslikTR,HaberIcerikTR")] Haber haber,HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;

                if (resim != null)
                {
                    using (var binaryReader = new BinaryReader(resim.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(resim.ContentLength);
                    }
                    haber.HaberResim = imageData;
                }

                haber.HaberTarih = DateTime.Now;
                haber.HaberBegenmeSayi = 0;
                var YazarID = Session["YazarID"];
                haber.YayimciId = Convert.ToInt32(YazarID);
                db.Haber.Add(haber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.HaberBlogTipId = new SelectList(db.HaberBlogTip, "Id", "HaberBlogTipi", haber.HaberBlogTipId);
            ViewBag.HaberkategoriId = new SelectList(db.HaberKategorileri, "Id", "HaberKategori", haber.HaberkategoriId);
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", haber.YayimciId);
            return View(haber);
        }

        // GET: Yazar/Habers/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.HaberBlogTipId = new SelectList(db.HaberBlogTip, "Id", "HaberBlogTipi");
            ViewBag.HaberkategoriId = new SelectList(db.HaberKategorileri, "Id", "HaberKategori", haber.HaberkategoriId);
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", haber.YayimciId);
            return View(haber);
        }

        // POST: Yazar/Habers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HaberBaslik,HaberIcerik,HaberTarih,HaberIzlenmeSayi,HaberBegenmeSayi,HaberkategoriId,HaberBlogTipId,HaberResim,YayimciId,HaberBaslikEN,HaberIcerikEN,HaberBaslikRU,HaberIcerikRU,HaberBaslikTR,HaberIcerikTR")] Haber haber, HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;

                if (resim != null)
                {
                    using (var binaryReader = new BinaryReader(resim.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(resim.ContentLength);
                    }
                    haber.HaberResim = imageData;
                }
                db.Entry(haber).State = EntityState.Modified;
                haber.HaberTarih = DateTime.Now;
                var YazarID = Session["YazarID"];
                haber.YayimciId = Convert.ToInt32(YazarID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HaberBlogTipId = new SelectList(db.HaberBlogTip, "Id", "HaberBlogTipi", haber.HaberBlogTipId);
            ViewBag.HaberkategoriId = new SelectList(db.HaberKategorileri, "Id", "HaberKategori", haber.HaberkategoriId);
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", haber.YayimciId);
            return View(haber);
        }

        // GET: Yazar/Habers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haber haber = db.Haber.Find(id);

            var haberler = db.Haber.Where(x=>x.Id==id);

            foreach(var i in haber.Yorumlar.ToList())
            {
                db.Yorumlar.Remove(i);
            }

            if (haber == null)  
            {
                return HttpNotFound();
            }
            db.Haber.Remove(haber);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
