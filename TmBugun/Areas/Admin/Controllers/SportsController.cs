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
    public class SportsController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Admin/Sports
        public ActionResult Index()
        {
            var sport = db.Sport.Include(s => s.SportBlogTip).Include(s => s.SportKategorileri).Include(s => s.Yayimci);
            return View(sport.ToList());
        }

        // GET: Admin/Sports/Details/5
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
            return View(sport);
        }

        // GET: Admin/Sports/Delete/5
        public ActionResult Delete(int? id)
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
