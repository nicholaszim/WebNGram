using BAL.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
	public class CategoryManager : BaseManager, ICategoryManager
	{
		public CategoryManager(IUnitOfWork uOW) : base(uOW)
		{

		}
		public void AddCategory(Example example, Category category)
		{
			example.Categories.Add(category);
		}
		public IEnumerable<Category> getCategories()
		{
			return uOW.CategoryRepo.Get();
		}
		public IEnumerable<string> getNamesOnly()
		{
			return uOW.CategoryRepo.Get().Select(p => p.Name);
		}
	}
}
