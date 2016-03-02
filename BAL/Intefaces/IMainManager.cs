using Models;
using System;
using System.Collections.Generic;

namespace BAL.Intefaces
{
	public interface IMainManager
	{
		void addExample(Example example);
		void buildExample(Func<string, IEnumerable<Ngram>> processor, string url, CategoryEnum category);
		float CompareExamples(Func<IEnumerable<Tuple<string, int>>, IEnumerable<Tuple<string, int>>, float> comparator, IEnumerable<Tuple<string, int>> a, IEnumerable<Tuple<string, int>> b);
		void deleteExampleById(int id);
		float GenCompareExample<T>(Func<T, T, float> comparator, T a, T b) where T : class;
		IEnumerable<Example> getAll();
		IEnumerable<Example> getByCategory(CategoryEnum category);
		Example toExample(CategoryEnum category, List<Ngram> ngrams);
	}
}
