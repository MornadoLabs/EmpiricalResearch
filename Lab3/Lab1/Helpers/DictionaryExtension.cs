using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab3.Models;

namespace Lab3.Helpers
{
    public static class DictionaryExtension
    {
        public static List<ChartViewModel> ToChartList<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            if (typeof(TKey) != typeof(int) && typeof(TKey) != typeof(double) &&
                typeof(TValue) != typeof(int) && typeof(TValue) != typeof(double))
            {
                return null;
            }

            return dictionary.Select(elem => new ChartViewModel { x = Convert.ToDouble(elem.Key), y = Convert.ToDouble(elem.Value) }).ToList();
        }
    }
}