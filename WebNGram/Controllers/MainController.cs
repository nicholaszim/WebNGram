using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using BAL.Manager;
using BAL.Intefaces;
using FBAL.Functions;
using Models;

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
			else return Json(new { success = true, examples }, JsonRequestBehavior.AllowGet);
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
		public JsonResult CompareExamples(int exampleId, int inputId)
		{
			var example = mainManager.getById(exampleId);
			var input = mainManager.getById(inputId);
			if (example == null || input == null) { return Json(new { success = false }, JsonRequestBehavior.AllowGet); }
			else
			{
				var exNgrams = Generics.mutateSeqBack(example.NGrams);
				var inNgrams = Generics.mutateSeqBack(input.NGrams);
				var result = mainManager.GenCompareExample<IEnumerable<Tuple<string, int>>>(Parallel.getDistanceP, exNgrams, inNgrams);
				return Json(new { success = true, result }, JsonRequestBehavior.AllowGet);
			}
		}

		public JsonResult CreateExample(int categoryid, string url, string name)
		{
			var category = categoryManager.getById(categoryid);
			if (category == null) return Json(new { success = false, message = "Category exists" }, JsonRequestBehavior.AllowGet);
			else
			{
				var status = mainManager.buildExample(Manager.ProcessP, url, category, name);
				if (status == Status.Error) return Json(new { success = false, message = "Example wasn`t created" }, JsonRequestBehavior.AllowGet);
				else return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
		}
		public JsonResult CreateCategory(string category)
		{
			var result = categoryManager.CreateCategory(category);
			if (result == Status.Warning)
			{
				return Json(new { success = false, message = "Category exists" }, JsonRequestBehavior.AllowGet);
			}
			else return Json(new { success = true, message = "Category created" }, JsonRequestBehavior.AllowGet);
		}

	}
}