using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
	public interface IUnitOfWork
	{
		IGenericRepository<Example> ExampleRepo { get; }

		void Dispose();
		void Save();
	}
}
