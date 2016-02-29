using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Example
	{
		public int Id { get; set; }
		public CategoryEnum Category { get; set; }
		public List<Tuple<string, int>> NGrams { get; set; }
	}
}
