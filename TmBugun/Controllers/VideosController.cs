using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TmBugun.Models;

namespace TmBugun.Controllers
{
    public class VideosController : Controller
    {
        TmBugunDB db = new TmBugunDB();

        // GET: Videolars
        public ActionResult AnaSayfa6()
        {
            var alti = db.Videolar.OrderByDescending(x => x.VideoEklenmeTarih).Take(6).ToList();
            return PartialView(alti.ToList());
        }
    }
}