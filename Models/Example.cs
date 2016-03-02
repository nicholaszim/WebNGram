using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Ngram
	{
		public int Id { get; set; }
		public string Key { get; set; }
		public int Value { get; set; }

		public virtual Example Example { get; set; }
	}

	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class Example
	{
		public int Id { get; set; }
		public CategoryEnum Category { get; set; }
		public virtual List<Ngram> NGrams { get; set; }
	}
}
