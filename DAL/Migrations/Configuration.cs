using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<DAL.MainContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(MainContext context)
		{
			var categories = new List<Category>
			{
				new Category() { Name = "Sport" },
				new Category() { Name = "Medicine" },
				new Category() { Name = "Space" },
				new Category() { Name = "IT" },
				new Category() { Name = "Movies" }
			};
			categories.ForEach(s => context.Categories.AddOrUpdate(p => p.Name, s));
			context.SaveChanges();
		}
	}
}
