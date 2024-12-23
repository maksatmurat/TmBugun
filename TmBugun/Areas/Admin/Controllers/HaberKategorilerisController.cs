using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;

namespace TmBugun.Areas.Admin.Controllers
{
    public class HaberKategorilerisController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Admin/HaberKategorileris
        public ActionResult Index()
        {
            return View(db.HaberKategorileri.ToList());
        }
        // GET: Admin/HaberKategorileris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HaberKategorileri haberKategorileri = db.HaberKategorileri.Find(id);
            var baglihaberler = db.Haber.Where(x => x.HaberkategoriId == id).ToList();
            string KategoriAd = db.Haber.Where(x => x.HaberkategoriId == id).FirstOrDefault()?.HaberKategorileri.HaberKategori;
            ViewData["kategori"] = KategoriAd;
            if (haberKategorileri == null)
            {
                return HttpNotFound();
            }
            return View(baglihaberler);
        }

        // GET: Admin/HaberKategorileris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HaberKategorileris/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HaberKategori,HaberKategoriEN,HaberKategoriRU,HaberKategoriTR,HaberKategoriLogo")] HaberKategorileri haberKategorileri)
        {
            if (ModelState.IsValid)
            {
                db.HaberKategorileri.Add(haberKategorileri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(haberKategorileri);
        }

        // GET: Admin/HaberKategorileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HaberKategorileri haberKategorileri = db.HaberKategorileri.Find(id);
            if (haberKategorileri == null)
            {
                return HttpNotFound();
            }
            return View(haberKategorileri);
        }

        // POST: Admin/HaberKategorileris/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HaberKategori,HaberKategoriEN,HaberKategoriRU,HaberKategoriTR,HaberKategoriLogo")] HaberKategorileri haberKategorileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(haberKategorileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(haberKategorileri);
        }

        // GET: Admin/HaberKategorileris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HaberKategorileri haberKategorileri = db.HaberKategorileri.Find(id);
            if (haberKategorileri == null)
            {
                return HttpNotFound();
            }
            db.HaberKategorileri.Remove(haberKategorileri);
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
