using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;


namespace TmBugun.Areas.Yazar.Controllers
{
    public class AnaSayfaController : Controller
    {
        TmBugunDB db = new TmBugunDB();
        // GET: Yazar/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AnaSayfaSec1()
        {
            var haber6 = db.Haber.OrderByDescending(x=>x.HaberTarih).Take(6).ToList();
            return PartialView(haber6);
        }
        public ActionResult AnaSayfaSec2()
        {
            var sport6 = db.Sport.OrderByDescending(x => x.SportTarih).Take(6).ToList();
            return PartialView(sport6);
        }
        public ActionResult AnaSayfaSec3()
        {
            var resim4 = db.Resimler.OrderByDescending(x => x.ResimEklenmeTarih).Take(4).ToList();
            return PartialView(resim4);
        }
        public ActionResult AnaSayfaSec4()
        {
            var video4 = db.Videolar.OrderByDescending(x => x.VideoEklenmeTarih).Take(4).ToList();
            return PartialView(video4);
        }
        public ActionResult Yorumlar_10()
        {
            int yazarid = Convert.ToInt32(Session["YazarID"]);
            var yorum10 = db.Yorumlar.Where(x => x.Haber.YayimciId == yazarid).Where(y=>y.Resimler.YayimciId==yazarid).Where(z=>z.Sport.YayimciId==yazarid).Take(10).ToList();
            return PartialView(yorum10);
        }
    }
}