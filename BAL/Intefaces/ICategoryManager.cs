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
		Status AddCategory(Example example, Category category);
		IEnumerable<Category> getCategories();
		IEnumerable<string> getNamesOnly();
		Status CreateCategory(string category);
		Category getById(int id);
	}
}
