using BAL.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using tupleSeq = System.Collections.Generic.IEnumerable<System.Tuple<string, int>>;

namespace BAL.Manager
{
	public class MainManager : BaseManager, IMainManager
	{
		public MainManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public void addExample(Example example)
		{
			uOW.ExampleRepo.Insert(example);
			uOW.Save();
		}
		public Example getById(int id)
		{
			return uOW.ExampleRepo.Get(ex => ex.ExampleId == id).FirstOrDefault();
		}

		public void deleteExampleById(int id)
		{
			var example = uOW.ExampleRepo.Get(p => p.ExampleId == id).FirstOrDefault();
			uOW.ExampleRepo.Delete(example);
		}

		public IEnumerable<Example> getAll()
		{
			return uOW.ExampleRepo.Get();
		}
		//public IEnumerable<Example> getByCategory(CategoryEnum category)
		//{
		//	return uOW.ExampleRepo.Get(p => p.Category == category);
		//}

		public IEnumerable<Example> getByCategory(string category)
		{
			return uOW.ExampleRepo.Get(p => p.Categories.Any(c => c.Name == category));
		}
		public Example toExample(string name, Category category, List<Ngram> ngram)
		{
			var Example = new Example();
			Example.Name = name;
			Example.Categories.Add(category);
			Example.NGrams.AddRange(ngram);
			return Example;
		}
		public Status buildExample(Func<string, IEnumerable<Ngram>> processor, string url, Category category, string name)
		{
			try
			{
				var ngrams = processor(url);
				var Example = toExample(name, category, ngrams.ToList());
				addExample(Example);
			}
			catch (Exception)
			{
				return Status.Error;
			}
			return Status.Success;
		}
		//public Example toExample(CategoryEnum category, List<Ngram> ngrams)
		//{
		//	return new Example { Category = category, NGrams = ngrams };
		//}

		//public void buildExample(Func<string, IEnumerable<Ngram>> processor, string url, CategoryEnum category)
		//{
		//	var ngrams = processor(url);
		//	var Example = toExample(category, ngrams.ToList());
		//	addExample(Example);
		//}

		public double CompareExamples(Func<tupleSeq, tupleSeq, double> comparator, tupleSeq a, tupleSeq b)
		{
			return comparator(a, b);
		}
		public double GenCompareExample<T>(Func<T, T, double> comparator, T a, T b) where T : class
		{
			return comparator(a, b);
		}
	}
}
