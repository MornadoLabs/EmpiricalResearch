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

        [Display(Name = "S₀²")]
        public double S0 { get; set; }
        [Display(Name = "S₁²")]
        public double S1 { get; set; }
        [Display(Name = "S₂²")]
        public double S2 { get; set; }

        [Display(Name = "Fₑₘ")]
        public double Fem { get; set; }
        [Display(Name = "Fₖₚ")]
        public double Fcr { get; set; }
    }
}