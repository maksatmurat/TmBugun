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
    public class ResimlersController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Yazar/Resimlers
        public ActionResult Index(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            int yazarid = Convert.ToInt32(Session["YazarID"]);
            var resimler = db.Resimler.Include(h => h.Yayimci).Where(x => x.YayimciId == yazarid).OrderByDescending(x => x.ResimEklenmeTarih).ToList().ToPagedList<Resimler>(_sayfaNo, 9);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Yazar/Views/Resimlers/ResimListesi.cshtml", resimler);
            }
            return View(resimler);
        }

        // GET: Yazar/Resimlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resimler resimler = db.Resimler.Find(id);
            if (resimler == null)
            {
                return HttpNotFound();
            }

            ViewData["resimlerId"] = resimler.Id;
            return View(resimler);
        }

        // GET: Yazar/Resimlers/Create
        public ActionResult Create()
        {
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad");
            return View();
        }

        // POST: Yazar/Resimlers/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResimEkle(Resimler resimler,HttpPostedFileBase resimoz)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;

                if (resimoz != null)
                {
                    using (var binaryReader = new BinaryReader(resimoz.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(resimoz.ContentLength);
                    }
                    resimler.Resim = imageData;
                }

                resimler.ResimEklenmeTarih = DateTime.Now;
                var YazarID = Session["YazarID"];
                resimler.ResimBegSayi = 0;
                resimler.YayimciId = Convert.ToInt32(YazarID);
                db.Resimler.Add(resimler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", resimler.YayimciId);
            return View(resimler);
        }

        // GET: Yazar/Resimlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resimler resimler = db.Resimler.Find(id);
            if (resimler == null)
            {
                return HttpNotFound();
            }
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", resimler.YayimciId);
            return View(resimler);
        }

        // POST: Yazar/Resimlers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ResimYazi,ResimBegSayi,YayimciId,ResimEklenmeTarih,Resim,ResimYaziEN,ResimYaziRU,ResimYaziTR")] Resimler resimler, HttpPostedFileBase resimoz)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;

                if (resimoz != null)
                {
                    using (var binaryReader = new BinaryReader(resimoz.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(resimoz.ContentLength);
                    }
                    resimler.Resim = imageData;
                }

                db.Entry(resimler).State = EntityState.Modified;
                resimler.ResimEklenmeTarih = DateTime.Now;
                var YazarID = Session["YazarID"];
                resimler.YayimciId = Convert.ToInt32(YazarID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", resimler.YayimciId);
            return View(resimler);
        }

        // GET: Yazar/Resimlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sport.Find(id);

            var sportlar = db.Sport.Where(x => x.Id == id);

            foreach (var i in sport.Yorumlar.ToList())
            {
                db.Yorumlar.Remove(i);
            }

            if (sport == null)
            {
                return HttpNotFound();
            }
            db.Sport.Remove(sport);
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
