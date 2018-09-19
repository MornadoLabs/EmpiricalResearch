using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1.Models
{
    public class ChartsOutputViewModel
    {
        public List<ChartViewModel> Sample { get; set; }
        public List<ChartViewModel> DiscreteRowByFrequency { get; set; }
        public List<ChartViewModel> DiscreteRowByVirtualFrequency { get; set; }
        public List<ChartViewModel> PolygonFrequency { get; set; }
        public List<ChartViewModel> PolygonVirtualFrequency { get; set; }
        public List<ChartViewModel> CumulusFrequency { get; set; }
        public List<ChartViewModel> CumulusVirtualFrequency { get; set; }
        public List<ChartViewModel> EmpericalFunction { get; set; }
    }
}