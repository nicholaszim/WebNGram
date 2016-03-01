﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using Models;
using FBAL.Functions;
using tupleSeq = System.Collections.Generic.IEnumerable<System.Tuple<string, int>>;

namespace BAL.Manager
{
	public class MainManager :BaseManager
	{
		public MainManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public void addExample(Example example)
		{
			uOW.ExampleRepo.Insert(example);
			uOW.Save();
		}

		public void deleteExampleById(int id)
		{
			var example = uOW.ExampleRepo.Get(p => p.Id == id).FirstOrDefault();
			uOW.ExampleRepo.Delete(example);
		}

		public IEnumerable<Example> getAll()
		{
			return uOW.ExampleRepo.Get();
		}
		public IEnumerable<Example> getByCategory(CategoryEnum category)
		{
			return uOW.ExampleRepo.Get(p => p.Category == category);
		}

		public Example toExample(CategoryEnum category, List<Ngram> ngrams)
		{
			return new Example { Category = category, NGrams = ngrams };
		}

		public void buildExample(Func<string, IEnumerable<Ngram>> processor, string url, CategoryEnum category)
		{
			var ngrams = processor(url);
			var Example = toExample(category, ngrams.ToList());
			addExample(Example);
		}

		public float CompareExamples(Func<tupleSeq, tupleSeq, float> comparator, tupleSeq a, tupleSeq b)
		{
			return comparator(a, b);
		}
	}
}
