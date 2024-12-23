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
    public class AnaSayfaController : Controller
    {
        private TmBugunDB db = new TmBugunDB();

        // GET: Admin/Home
        public ActionResult Index()
        {
          
            return View();
        }     
        public ActionResult Haberler()
        {
            var haberler = db.Haber.Take(20);
            return PartialView(haberler.ToList());
        }
        public ActionResult Yazarlar()
        {
            var yazarlar = db.Yayimci;
            return PartialView(yazarlar.ToList());
        }
   
    }
}