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
    public class HabersController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Admin/Habers
        public ActionResult Index()
        {
            var haber = db.Haber.Include(h => h.HaberBlogTip).Include(h => h.HaberKategorileri).Include(h => h.Yayimci);
            return View(haber.ToList());
        }

        // GET: Admin/Habers/Details/5
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
            return View(haber);
        }

     
        // GET: Admin/Habers/Delete/5
        public ActionResult Delete(int? id)
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
