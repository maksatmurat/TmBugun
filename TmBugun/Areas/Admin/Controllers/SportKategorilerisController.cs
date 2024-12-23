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
    public class SportKategorilerisController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Admin/SportKategorileris
        public ActionResult Index()
        {
            return View(db.SportKategorileri.ToList());
        }

        // GET: Admin/SportKategorileris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportKategorileri sportKategorileri = db.SportKategorileri.Find(id);
            var baglihaberler = db.Sport.Where(x => x.SportkategoriId == id).ToList();
            string KategoriAd = db.Sport.Where(x => x.SportkategoriId == id).FirstOrDefault()?.SportKategorileri.SportKategori;
            ViewData["kategori"] = KategoriAd;
            if (sportKategorileri == null)
            {
                return HttpNotFound();
            }
            return View(baglihaberler);
        }

        // GET: Admin/SportKategorileris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SportKategorileris/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SportKategori,SportKategoriLogo,SportKategoriEN,SportKategoriRU,SportKategoriTR")] SportKategorileri sportKategorileri)
        {
            if (ModelState.IsValid)
            {
                db.SportKategorileri.Add(sportKategorileri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sportKategorileri);
        }

        // GET: Admin/SportKategorileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportKategorileri sportKategorileri = db.SportKategorileri.Find(id);
            if (sportKategorileri == null)
            {
                return HttpNotFound();
            }
            return View(sportKategorileri);
        }

        // POST: Admin/SportKategorileris/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SportKategori,SportKategoriLogo,SportKategoriEN,SportKategoriRU,SportKategoriTR")] SportKategorileri sportKategorileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sportKategorileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sportKategorileri);
        }

        // GET: Admin/SportKategorileris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SportKategorileri sportKategorileri = db.SportKategorileri.Find(id);
            if (sportKategorileri == null)
            {
                return HttpNotFound();
            }
            db.SportKategorileri.Remove(sportKategorileri);
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
