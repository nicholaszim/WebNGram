using BAL.Intefaces;
using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repositories
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private MainContext context;

		private bool disposed = false;

		private IGenericRepository<Example> exampleRepo;
		private IGenericRepository<Category> categoryRepo;

		public IGenericRepository<Example> ExampleRepo
		{
			get
			{
				if (exampleRepo == null)
				{
					exampleRepo = new BaseRepository<Example>(context);
				}
				return exampleRepo;
			}
		}

		public IGenericRepository<Category> CategoryRepo
		{
			get
			{
				if (categoryRepo == null)
				{
					categoryRepo = new BaseRepository<Category>(context);
				}
				return categoryRepo;
			}
		}

		public UnitOfWork()
		{
			context = new MainContext();
			exampleRepo = new BaseRepository<Example>(context);
			categoryRepo = new BaseRepository<Category>(context);
		}

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Save()
		{
			context.SaveChanges();
		}
	}
}
