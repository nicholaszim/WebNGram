﻿using BAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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