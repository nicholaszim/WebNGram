using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using BAL.Manager;

namespace WebNGram.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult GetAllBy(string category)
		{
			return null;
		}
    }
}