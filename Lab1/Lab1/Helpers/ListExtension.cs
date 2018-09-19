using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab1.Models;

namespace Lab1.Helpers
{
    public static class ListExtension
    {
        public static List<ChartViewModel> ToChartList<T>(this List<T> list)
        {
            if (typeof(T) != typeof(int) && typeof(T) != typeof(double))
            {
                return null;
            }

            var result = new List<ChartViewModel>();

            for (int i = 0; i < list.Count; i++)
            {
                result.Add(new ChartViewModel { x = i + 1, y = Convert.ToDouble(list[i]) });
            }

            return result;
        }
    }
}