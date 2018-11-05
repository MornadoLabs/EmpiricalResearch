using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab3.Models;

namespace Lab3.Helpers
{
    public static class ChartListHalper
    {
        public static List<ChartViewModel> Round(this List<ChartViewModel> list)
        {
            foreach(var elem in list)
            {
                elem.x = Double.Parse(elem.x.ToString("N2"));
                elem.y = Double.Parse(elem.y.ToString("N2"));
            }

            return list;
        }
    }
}