using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;

namespace WebNGram.Controllers
{
    public class BaseController : Controller
    {
		protected UnitOfWork uOW;

		public BaseController()
		{
			uOW = new UnitOfWork();
		}
    }
}