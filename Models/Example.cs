using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public int CategoryId { get; set; }
		public string Name { get; set; }

		public virtual List<Example> Examples { get; set; }
	}

	public class Example
	{
		public int ExampleId { get; set; }
		public string Name { get; set; }
		public virtual List<Ngram> NGrams { get; set; }
		public virtual List<Category> Categories { get; set; }
	}
}
