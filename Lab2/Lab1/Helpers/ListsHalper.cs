using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab2.Models;

namespace Lab2.Helpers
{
    public static class ListsHalper
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

        public static List<TableViewModel> Round(this List<TableViewModel> list)
        {
            foreach(var elem in list)
            {
                elem.y = Double.Parse(elem.y.ToString("N2"));
            }

            return list;
        }
    }
}