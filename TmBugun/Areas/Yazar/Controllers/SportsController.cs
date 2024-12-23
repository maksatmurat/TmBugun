using PagedList;
using PagedList.Mvc;
using System;
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
    public class SportsController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Yazar/Sports
        public ActionResult Index(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            int yazarid = Convert.ToInt32(Session["YazarID"]);
            var sport = db.Sport.Include(h => h.SportBlogTip).Include(h => h.SportKategorileri).Include(h => h.Yayimci).Where(x => x.YayimciId == yazarid).OrderByDescending(x => x.SportTarih).ToList().ToPagedList<Sport>(_sayfaNo, 9);
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Yazar/Views/Sports/SportListesi.cshtml", sport);
            }
            return View(sport);
        }

        // GET: Yazar/Sports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sport.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }

            ViewData["sportId"] = sport.Id;
            return View(sport);
        }

        // GET: Yazar/Sports/Create
        public ActionResult Create()
        {
            ViewBag.SportBlogTipId = new SelectList(db.SportBlogTip, "Id", "SportBlogTipi");
            ViewBag.SportkategoriId = new SelectList(db.SportKategorileri, "Id", "SportKategori");
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad");
            return View();
        }

        // POST: Yazar/Sports/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SportEkle(Sport sport, HttpPostedFileBase resim)
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
                    sport.SportResim = imageData;
                }

                sport.SportBegenmeSayi = 0;
                sport.SportTarih = DateTime.Now;
                var YazarID = Session["YazarID"];
                sport.YayimciId = Convert.ToInt32(YazarID);
                db.Sport.Add(sport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.SportBlogTipId = new SelectList(db.SportBlogTip, "Id", "SportBlogTipi", sport.SportBlogTipId);
            ViewBag.SportkategoriId = new SelectList(db.SportKategorileri, "Id", "SportKategori", sport.SportkategoriId);
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", sport.YayimciId);
            return View(sport);
        }

        // GET: Yazar/Sports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sport.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            ViewBag.SportBlogTipId = new SelectList(db.SportBlogTip, "Id", "SportBlogTipi");
            ViewBag.SportkategoriId = new SelectList(db.SportKategorileri, "Id", "SportKategori", sport.SportkategoriId);
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", sport.YayimciId);
            return View(sport);
        }

        // POST: Yazar/Sports/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SportBaslik,SportIcerik,SportTarih,SportIzlenmeSayi,SportBegenmeSayi,SportkategoriId,SportBlogTipId,SportResim,YayimciId")] Sport sport, HttpPostedFileBase resim)
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
                    sport.SportResim = imageData;
                }
                db.Entry(sport).State = EntityState.Modified;
                sport.SportTarih = DateTime.Now;
                var YazarID = Session["YazarID"];
                sport.YayimciId = Convert.ToInt32(YazarID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SportBlogTipId = new SelectList(db.SportBlogTip, "Id", "SportBlogTipi", sport.SportBlogTipId);
            ViewBag.SportkategoriId = new SelectList(db.SportKategorileri, "Id", "SportKategori", sport.SportkategoriId);
            ViewBag.YayimciId = new SelectList(db.Yayimci, "Id", "Yayimci_ad", sport.YayimciId);
            return View(sport);
        }
        // GET: Yazar/Habers/Delete/5
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
