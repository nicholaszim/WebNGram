using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Repository
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private MainContext context;

		private bool disposed = false;

		private IGenericRepository<Example> exampleRepo;

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

		public UnitOfWork()
		{
			context = new MainContext();
			exampleRepo = new BaseRepository<Example>(context);
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
