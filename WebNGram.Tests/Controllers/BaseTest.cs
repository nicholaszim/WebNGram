using BAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNGram.Tests.Controllers
{
	public class BaseTest
	{
		protected UnitOfWork uOW;

		public BaseTest()
		{
			uOW = new UnitOfWork();
		}
	}
}
