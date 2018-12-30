using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab6.Models
{
    public class OutputViewModel
    {
        public double Z { get; set; }

        public double Kxy { get; set; }
        public double S0x { get; set; }
        public double S0y { get; set; }
        
        public double Rxy { get; set; }
        public double R1xy001 { get; set; }
        public double R2xy001 { get; set; }
        public double R1xy005 { get; set; }
        public double R2xy005 { get; set; }
               
        public double RomCoef { get; set; }
    }
}