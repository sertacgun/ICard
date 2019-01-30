using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICard.Controllers
{
    public class DetailsController : BaseController
    {
        // GET: Details
        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(string id)
        {
             var f = db.Firmalar.Where(x=> x.UserId == id).ToList();
            return View();
        }

    }
}