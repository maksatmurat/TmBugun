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
    public class AdminBilgilerisController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Admin/AdminBilgileris
        public ActionResult Index()
        {
            return View(db.AdminBilgileri.ToList());
        }
        // GET: Admin/AdminBilgileris/Details/5
       

        // GET: Admin/AdminBilgileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminBilgileri adminBilgileri = db.AdminBilgileri.Find(id);
            if (adminBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(adminBilgileri);
        }

        // POST: Admin/AdminBilgileris/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdminAd,Adminsifre")] AdminBilgileri adminBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminBilgileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminBilgileri);
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
