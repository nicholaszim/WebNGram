using Models;
using System;
using System.Collections.Generic;

namespace BAL.Intefaces
{
	public interface IMainManager
	{
		void addExample(Example example);
		Example getById(int id);
		Status buildExample(Func<string, IEnumerable<Ngram>> processor, string url, Category category, string name);
		double CompareExamples(Func<IEnumerable<Tuple<string, int>>, IEnumerable<Tuple<string, int>>, double> comparator, IEnumerable<Tuple<string, int>> a, IEnumerable<Tuple<string, int>> b);
		void deleteExampleById(int id);
		double GenCompareExample<T>(Func<T, T, double> comparator, T a, T b) where T : class;
		IEnumerable<Example> getAll();
		IEnumerable<Example> getByCategory(string categor);
		Example toExample(string name, Category category, List<Ngram> ngram);
	}
}
