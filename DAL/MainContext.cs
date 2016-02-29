using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class MainContext : DbContext
	{
		public MainContext() : base("Ngram")
		{
			this.Configuration.LazyLoadingEnabled = true;
		}

		public MainContext(string connString) : base(connString)
		{
			this.Configuration.LazyLoadingEnabled = true;
		}

		public DbSet<Ngram> Ngrams { get; set;}
		public DbSet<Example> Examples { get; set; }
	}
}
