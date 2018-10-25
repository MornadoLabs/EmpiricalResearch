using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
    public class IntervalModel
    {
        public double IntervalStart { get; set; }
        public double IntervalEnd { get; set; }
        public List<double> IntervalData { get; set; }

        public string Name => $"[ {IntervalStart.ToString("N2")}; {IntervalEnd.ToString("N2")} )";

        public double DiscreteValue => (IntervalStart + IntervalEnd) / 2;

        public bool HasElement(double element)
        {
            return IntervalStart <= element && IntervalEnd > element;
        }
    }
}