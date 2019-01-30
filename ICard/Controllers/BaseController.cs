using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICard.Controllers
{
    public class BaseController : Controller
    {
        public iKartvizitEntities db = new iKartvizitEntities();
    }
}