using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab5.Models
{
    public class OutputViewModel
    {
        public List<InputTableViewModel> InputTable { get; set; }

        public double Q { get; set; }
        [Display(Name = "Q₁")]
        public double Q1 { get; set; }
        [Display(Name = "Q₂")]
        public double Q2 { get; set; }
        [Display(Name = "Q₃")]
        public double Q3 { get; set; }

        [Display(Name = "S²")]
        public double S { get; set; }
        [Display(Name = "S₁²")]
        public double S1 { get; set; }
        [Display(Name = "S₂²")]
        public double S2 { get; set; }
        [Display(Name = "S₃²")]
        public double S3 { get; set; }

        [Display(Name = "FₑₘA")]
        public double FemA { get; set; }
        [Display(Name = "FₑₘB")]
        public double FemB { get; set; }

        [Display(Name = "FₖₚA")]
        public double FcrA001 { get; set; }
        [Display(Name = "FₖₚB")]
        public double FcrB001 { get; set; }

        [Display(Name = "FₖₚA")]
        public double FcrA005 { get; set; }
        [Display(Name = "FₖₚB")]
        public double FcrB005 { get; set; }
    }
}