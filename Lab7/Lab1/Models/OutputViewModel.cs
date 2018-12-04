using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab7.Models
{
    public class OutputViewModel
    {
        public ChartViewModel[][] SqrMethodChartData { get; set; }
        public ChartViewModel[][] CoefMethodChartData { get; set; }

        public double Delta { get; set; }
        public double DeltaAlpha { get; set; }
        public double DeltaBetha { get; set; }

        public double NotDelta { get; set; }
        public double NotDeltaAlpha { get; set; }
        public double NotDeltaBetha { get; set; }

        public double Alpha { get; set; }
        public double Betha { get; set; }

        public double NotAlpha { get; set; }
        public double NotBetha { get; set; }

        public double Ryorx { get; set; }
        public double Rxory { get; set; }

        public double MiddleXValue { get; set; }
        public double MiddleYValue { get; set; }
    }
}