using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;
using PagedList;


namespace TmBugun.Controllers
{
    public class HomeController : Controller
    {
        TmBugunDB db = new TmBugunDB();
        // GET: Home
        public ActionResult Index()
        {
            ViewData["KullaniciAd"] = Session["NormalAdi"];
            return View();
        }
        public ActionResult AnaSayfaSec1()
        {
            var tekli = db.Haber.Where(x => x.HaberBlogTipId == 1).Take(1).ToList();
            return PartialView(tekli);
        }
        public ActionResult AnaSayfaSec1IkinciKisim()
        {
            var ikili = db.Haber.Where(x => x.HaberBlogTipId == 2).Take(4).ToList();
            return PartialView(ikili);
        }
        public ActionResult AnaSayfaSec2()
        {
            var uclu = db.Haber.Where(x => x.HaberBlogTipId == 3).Take(6).ToList();
            return PartialView(uclu);
        }
        public ActionResult AnaSayfaSec3()
        {
            var uclu = db.Sport.Where(x => x.SportBlogTipId == 3).Take(6).ToList();
            return PartialView(uclu);
        }
        public ActionResult HowaDurum()
        {
            return PartialView();
        }
        public ActionResult Search()
        {
            return PartialView();
        }
        public ActionResult IletisimBilgileri()
        {
            var iletisimbil = db.IletisimBilgileri.ToList();
            return PartialView(iletisimbil);
        }

    }

}