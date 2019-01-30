using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer;

namespace ICard.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int id)
        {
            Firmalar Firma = new Firmalar();
            Firma = db.Firmalar.Find(id);
            return View(Firma);
        }
       




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}