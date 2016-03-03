using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using BAL.Manager;
using BAL.Intefaces;

namespace WebNGram.Controllers
{
    public class MainController : BaseController
    {
		private IMainManager mainManager;
		private ICategoryManager categoryManager;
		public MainController(IMainManager mainManager, ICategoryManager categoryManager)
		{
			this.mainManager = mainManager;
			this.categoryManager = categoryManager;
		}
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult GetAllBy(string category)
		{
			var examples = mainManager.getByCategory(category);
			if (examples == null)
			{
				return Json(new { success = false }, JsonRequestBehavior.AllowGet);
			}
			else return Json(new { success = true, examples}, JsonRequestBehavior.AllowGet);
		}
		public JsonResult GetCategories()
		{
			var categories = categoryManager.getCategories();
			if (categories == null)
			{
				return Json(new { success = false }, JsonRequestBehavior.AllowGet);
			}
			else return Json(new { success = true, categories }, JsonRequestBehavior.AllowGet);
		}

    }
}