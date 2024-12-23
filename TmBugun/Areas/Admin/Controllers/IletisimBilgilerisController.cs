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
    public class IletisimBilgilerisController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Admin/IletisimBilgileris
        public ActionResult Index()
        {
            return View(db.IletisimBilgileri.ToList());
        }

        // GET: Admin/IletisimBilgileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IletisimBilgileri iletisimBilgileri = db.IletisimBilgileri.Find(id);
            if (iletisimBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(iletisimBilgileri);
        }

        // POST: Admin/IletisimBilgileris/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Facebook,Instagram,Twitter,Eposta,Adres,Hakkimizda,HakkimizdaEN,HakkimizdaRU,HakkimizdaTR")] IletisimBilgileri iletisimBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iletisimBilgileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iletisimBilgileri);
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
