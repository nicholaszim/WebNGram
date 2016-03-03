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
		public Status AddCategory(Example example, Category category)
		{
			try
			{
				example.Categories.Add(category);
			}
			catch (Exception)
			{
				return Status.Error;
			}
			return Status.Success;
		}
		public IEnumerable<Category> getCategories()
		{
			return uOW.CategoryRepo.Get();
		}
		public IEnumerable<string> getNamesOnly()
		{
			return uOW.CategoryRepo.Get().Select(p => p.Name);
		}
		public Status CreateCategory(string category)
		{
			var check = uOW.CategoryRepo.Get().Any(c => c.Name == category);
			if (!check) return Status.Warning;
			else
			{
				uOW.CategoryRepo.Insert(new Category { Name = category });
				return Status.Success;
			}
		}
		public Category getById(int id)
		{
			return uOW.CategoryRepo.Get(c => c.CategoryId == id).FirstOrDefault();
		}
	}
}
