using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5.Models
{
    public class InputTableViewModel
    {
        public int? Time { get; set; }
        public double Experiment1 { get; set; }
        public double Experiment2 { get; set; }
        public double Experiment3 { get; set; }
        public double Experiment4 { get; set; }
        public double MiddleValue { get; set; }
    }
}