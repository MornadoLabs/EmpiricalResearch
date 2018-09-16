using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab1.Models
{
    public class EmpericalSampleViewModel
    {
        public bool IsVirtual { get; set; }

        [Display(Name = "Дискретний емпіричний ряд за частотами")]
        public Dictionary<double, int> DiscreteRowByFrequency { get; set; }

        [Display(Name = "Дискретний емпіричний ряд за відносними частотами")]
        public Dictionary<double, double> DiscreteRowByVirtualFrequency { get; set; }
        
        [Display(Name = "Середнє емпіричне значення")]
        public double MiddleEmpericalValue { get; set; }

        [Display(Name = "Відхилення")]
        public List<double> Deviations { get; set; }

        [Display(Name = "Мода")]
        public List<double> Mode { get; set; }

        [Display(Name = "Медіана")]
        public double Median { get; set; }

        [Display(Name = "Дисперсія")]
        public double Dispersion { get; set; }

        [Display(Name = "Середнє квадратичне відхилення")]
        public double MiddleStandardDeviation { get; set; }

        [Display(Name = "Розмах")]
        public double Swing { get; set; }

        [Display(Name = "Коефіцієнт варіації")]
        public double VariationCoef { get; set; }

        [Display(Name = "Коефіцієнт асиметрії")]
        public double AsymmetricalCoef { get; set; }

        [Display(Name = "Ексцес")]
        public double Excess { get; set; }
    }
}