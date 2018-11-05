using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class IntervalModel
    {
        private double _start;
        public double Start
        {
            get => _start;
            set => _start = Math.Round(value, 5);
        }

        private double _end;
        public double End
        {
            get => _end;
            set => _end = Math.Round(value, 5);
        }
    }
}