using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;
using PagedList.Mvc;
using PagedList;

namespace TmBugun.Areas.Yazar.Controllers
{
    public class VideolarsController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Yazar/Videolars
        public ActionResult Index(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            int yazarid = Convert.ToInt32(Session["YazarID"]);
            var videolar = db.Videolar.Include(v => v.Yayimci).Where(x=>x.YayimciId==yazarid).OrderByDescending(x => x.VideoEklenmeTarih).ToList().ToPagedList<Videolar>(_sayfaNo, 9);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Yazar/Views/Videolars/VideoListesi.cshtml", videolar);
            };
            return View(videolar);
        }

        // GET: Yazar/Videolars/Create
        public ActionResult Create()
        {
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad");
            return View();
        }

        // POST: Yazar/Videolars/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VideoBasligi,VideoBegeniSayi,VideoEklenmeTarih,VideoYol,YayimciId,VideoBasligiEN,VideoBasligiRU,VideoBasligiTR")] Videolar videolar)
        {
            if (ModelState.IsValid)
            {
                videolar.VideoBegeniSayi = 0;
                videolar.VideoEklenmeTarih = DateTime.Now;
                var YazarID = Session["YazarID"];
                videolar.YayimciId = Convert.ToInt32(YazarID);
                db.Videolar.Add(videolar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", videolar.YayimciId);
            return View(videolar);
        }

        // GET: Yazar/Videolars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videolar videolar = db.Videolar.Find(id);
            if (videolar == null)
            {
                return HttpNotFound();
            }
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", videolar.YayimciId);
            return View(videolar);
        }

        // POST: Yazar/Videolars/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VideoBasligi,VideoBegeniSayi,VideoEklenmeTarih,VideoYol,YayimciId,VideoBasligiEN,VideoBasligiRU,VideoBasligiTR")] Videolar videolar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videolar).State = EntityState.Modified;
                videolar.VideoEklenmeTarih = DateTime.Now;
                var YazarID = Session["YazarID"];
                videolar.YayimciId = Convert.ToInt32(YazarID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", videolar.YayimciId);
            return View(videolar);
        }

        // GET: Yazar/Videolars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Videolar videolar = db.Videolar.Find(id);

            if (videolar == null)
            {
                return HttpNotFound();
            }
            db.Videolar.Remove(videolar);
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
