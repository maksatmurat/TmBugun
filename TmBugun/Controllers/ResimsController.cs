using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;

namespace TmBugun.Controllers
{
    public class ResimsController : Controller
    {
        TmBugunDB db = new TmBugunDB();
        // GET: Resimlers
        public ActionResult AnaSayfa4()
        {
            var dortlu = db.Resimler.OrderByDescending(m => m.ResimEklenmeTarih).Take(4).ToList();
            return PartialView(dortlu);
        }
        public ActionResult AnaSayfa()
        {
            var resimler = db.Resimler.OrderByDescending(m => m.ResimEklenmeTarih).ToList();
            return View(resimler);
        }
    }
}