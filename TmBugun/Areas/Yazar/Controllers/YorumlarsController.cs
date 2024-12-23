using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;

namespace TmBugun.Areas.Yazar.Controllers
{
    public class YorumlarsController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Yazar/Yorumlars
        public ActionResult HaberYorumlari(int id)
        {
            //var yorumlar = db.Yorumlar.Include(y => y.Haber).Include(y => y.NormalKullanici);
            var tumyorum = db.Yorumlar.Where(x => x.HaberID == id).ToList();
            return PartialView(tumyorum);
        }
        public ActionResult SportYorumlari(int id)
        {
            var yorumlar = db.Yorumlar.Include(y => y.NormalKullanici).Include(y => y.Sport);
            var tumyorum = yorumlar.Where(x => x.SportID == id).ToList();
            return PartialView(tumyorum);
        }
        public ActionResult ResimYorumlari(int id)
        {
            var yorumlar = db.Yorumlar.Include(y => y.NormalKullanici).Include(y => y.Resimler);
            var tumyorum = yorumlar.Where(x => x.ResimID == id).ToList();
            return PartialView(tumyorum);
        }


      [HttpPost]
        public JsonResult Delete(int? id)
        {
            var yorum = db.Yorumlar.Find(id);
            db.Yorumlar.Remove(yorum);
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
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
