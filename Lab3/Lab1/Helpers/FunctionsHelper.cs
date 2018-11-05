using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Helpers
{
    public static class FunctionsHelper
    {
        public static Dictionary<double, double> LaplasFunction = new Dictionary<double, double>
        {
            { 0.4995, 3.4 },
            { 0.495, 2.58 },
            { 0.475, 1.96 }
        };

        public static Dictionary<double, double> StudentFunction = new Dictionary<double, double>
        {
            { 0.4995, 3.403 },
            { 0.495, 2.633 },
            { 0.475, 1.987 }
        };

        public static Dictionary<double, double> PirsonFunction = new Dictionary<double, double>
        {
            { 0.4995, 0.29 },
            { 0.495, 0.211 },
            { 0.475, 0.151 }
        };
    }
}