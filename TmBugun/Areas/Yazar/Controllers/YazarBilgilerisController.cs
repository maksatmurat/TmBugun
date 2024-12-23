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

namespace TmBugun.Areas.Yazar.Controllers
{
    public class YazarBilgilerisController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Yazar/YazarBilgileris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yayimci yayimci = db.Yayimci.Find(id);
            if (yayimci == null)
            {
                return HttpNotFound();
            }
            return View(yayimci);
        }
        public ActionResult ResimGetir(int id)
        {
            Yayimci yazar = db.Yayimci.Find(id);
            if (yazar.YayimciResim==null)
            {
                string yol = "~/Areas/assets/img/faces/writingclub.png";
                return File(yol, "image/jpeg");
            }
            else
            {
                byte[] resim = db.Yayimci.Where(x => x.Id == id).Select(img => img.YayimciResim).FirstOrDefault();
                return File(resim, "imaga/jpeg");
             
            }
        }
        // GET: Yazar/YazarBilgileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yayimci yayimci = db.Yayimci.Find(id);
            if (yayimci == null)
            {
                return HttpNotFound();
            }
            return PartialView(yayimci);
        }

        // POST: Yazar/YazarBilgileris/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Yayimci_ad,Yayimci_soyad,Yayimci_eposta,Yayimci_sifre,YayimciResim,YayimciYas,YayimciFacebook,YayimciTwitter")] Yayimci yayimci, HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                if (yayimci.YayimciFacebook == null)
                {
                    yayimci.YayimciFacebook = "https://www.facebook.com/tmbugun.tmbugun.5";
                }
                if (yayimci.YayimciTwitter == null)
                {
                    yayimci.YayimciTwitter = "https://twitter.com/BugunTm";
                }

                byte[] imageData = null;

                if (resim != null)
                {
                    using (var binaryReader = new BinaryReader(resim.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(resim.ContentLength);
                    }
                    yayimci.YayimciResim = imageData;
                }

                db.Entry(yayimci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","YazarBilgileris", new { id=yayimci.Id});
            }
            return PartialView(yayimci);
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
