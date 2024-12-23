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

namespace TmBugun.Areas.Admin.Controllers
{
    public class YayimcisController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Admin/Yayimcis
        public ActionResult Index()
        {
            return View(db.Yayimci.ToList());
        }
      
        // GET: Admin/Yayimcis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yayimci yayimci = db.Yayimci.Find(id);
            ViewData["yazarId"] = yayimci.Id;
            if (yayimci == null)
            {
                return HttpNotFound();
            }
            return View(yayimci);
        }

        // GET: Admin/Yayimcis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Yayimcis/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Yayimci_ad,Yayimci_soyad,Yayimci_eposta,Yayimci_sifre,YayimciResim,YayimciYas,YayimciFacebook,YayimciTwitter")] Yayimci yayimci,HttpPostedFileBase resim)
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
                    yayimci.YayimciResim = imageData;
                }
                if(yayimci.YayimciFacebook==null)
                {
                    yayimci.YayimciFacebook = "https://www.facebook.com/tmbugun.tmbugun.5";
                }   
                if(yayimci.YayimciTwitter==null)
                {
                    yayimci.YayimciTwitter = "https://twitter.com/BugunTm";
                }
                db.Yayimci.Add(yayimci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yayimci);
        }

        // GET: Admin/Yayimcis/Edit/5
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
            return View(yayimci);
        }

        // POST: Admin/Yayimcis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Yayimci_ad,Yayimci_soyad,Yayimci_eposta,Yayimci_sifre,YayimciResim,YayimciYas,YayimciFacebook,YayimciTwitter")] Yayimci yayimci, HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (resim!=null)
                {
                    using (var binaryReader = new BinaryReader(resim.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(resim.ContentLength);
                    }
                    yayimci.YayimciResim = imageData;
                }
                else if(resim == null)
                {
                    yayimci.YayimciResim = yayimci.YayimciResim;
                }
                if (yayimci.YayimciFacebook == null)
                {
                    yayimci.YayimciFacebook = "https://www.facebook.com/tmbugun.tmbugun.5";
                }
                if (yayimci.YayimciTwitter == null)
                {
                    yayimci.YayimciTwitter = "https://twitter.com/BugunTm";
                }
                db.Entry(yayimci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yayimci);
        }

        // GET: Admin/Yayimcis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yayimci yayimci = db.Yayimci.Find(id);
            int baglihaber = db.Haber.Where(x => x.YayimciId == id).Count();
            if(baglihaber!=0)
            {
                var haberler = db.Haber.Where(x => x.YayimciId == id);
                foreach(var item in haberler)
                {
                    item.YayimciId = 2;
                }
                db.SaveChanges();
            }  
            int baglisport = db.Sport.Where(x => x.YayimciId == id).Count();
            if(baglisport!=0)
            {
                var sportlar = db.Sport.Where(x => x.YayimciId == id);
                foreach(var item in sportlar)
                {
                    item.YayimciId = 2;
                }
                db.SaveChanges();
            }
            if (yayimci == null)
            {
                return HttpNotFound();
            }
            db.Yayimci.Remove(yayimci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult son2haber(int id)
        {
            var haberler = db.Haber.Where(x => x.YayimciId == id).Take(2).ToList();
            return PartialView(haberler);
        }
        public ActionResult bagliHaberler(int id)
        {
            var haberler = db.Haber.Where(x => x.YayimciId == id).ToList();
            return PartialView(haberler);
        }
        public ActionResult son2Sport(int id)
        {
            var sportlar = db.Sport.Where(x => x.YayimciId == id).Take(2).ToList();
            return PartialView(sportlar);
        }
        public ActionResult bagliSportHaberler(int id)
        {
            var sportlar = db.Sport.Where(x => x.YayimciId == id).ToList();
            return PartialView(sportlar);
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
