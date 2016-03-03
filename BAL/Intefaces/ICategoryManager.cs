using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Intefaces
{
	public interface ICategoryManager
	{
		void AddCategory(Example example, Category category);
		IEnumerable<Category> getCategories();
		IEnumerable<string> getNamesOnly();
	}
}
