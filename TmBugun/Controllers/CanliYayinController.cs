using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TmBugun.Controllers
{
    public class CanliYayinController : Controller
    {
        // GET: CanliYayin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AltynAsyr()
        {
            return PartialView();
        }
        public ActionResult Yaslyk()
        {
            return PartialView();
        }
    }
}