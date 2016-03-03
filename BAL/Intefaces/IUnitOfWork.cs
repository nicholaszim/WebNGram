using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Intefaces
{
	public interface IUnitOfWork
	{
		IGenericRepository<Example> ExampleRepo { get; }
		IGenericRepository<Category> CategoryRepo { get; }

		void Dispose();
		void Save();
	}
}
