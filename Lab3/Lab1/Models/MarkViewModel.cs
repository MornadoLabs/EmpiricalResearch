using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class MarkViewModel
    {
        [Display(Name = "Рівень значущості")]
        public double Importance { get; set; }
        
        public IntervalModel IntervalMarkForKnownDispersion { get; set; }
        public IntervalModel IntervalMarkForUnknownDispersion { get; set; }        
        public IntervalModel IntervalMarkMiddleStandardDeviation { get; set; }
    }
}