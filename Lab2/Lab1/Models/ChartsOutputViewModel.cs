using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
    public class ChartsOutputViewModel
    {
        public List<TableViewModel> DiscreteRowByFrequency { get; set; }
        public List<TableViewModel> IntervalRowByFrequency { get; set; }
        public List<TableViewModel> IntervalRowByVirtualFrequency { get; set; }
        public List<TableViewModel> HistogramByFrequency { get; set; }
        public List<TableViewModel> HistogramByVirtualFrequency { get; set; }
        public List<ChartViewModel> CumulusFrequency { get; set; }
        public List<ChartViewModel> CumulusVirtualFrequency { get; set; }
        public List<ChartViewModel> EmpericalFunction { get; set; }
    }
}