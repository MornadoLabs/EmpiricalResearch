using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1.Repositories
{
    internal class EmpericalSampleRepository
    {
        private List<double> EmpericalSample = new List<double>
        {
            -5, -4.6, -4.6, -4.6, -4.4, -4.4, -4.4, -4.4, -4.2, -4.2, -4.2, -4.2, -4.2, -4, -3.8, -3.8, -3.6, -3.4, -3.2,
            -3, -2.9, -2.7, -2.5, -2.5, -2.3, -2.1, -2.1, -2, -1.6, -1.6, -1.6, -1.4, -1.4, -1.4, -1.4, -1.2, -1.2, -1.2,
            -1.2, -1.2, -1, -0.7, -0.7, -0.3, -0.3, 0, 0.3, 0.3, 0.7, 0.7, 1, 1.2, 1.2, 1.2, 1.2, 1.2, 1.2, 1.4, 1.4, 1.4,
            1.4, 1.6, 1.6, 1.6, 2, 2.1, 2.1, 2.3, 2.5, 2.7, 2.9, 3, 3.2, 3.4, 3.6, 3.8, 3.8, 4, 4.2, 4.2, 4.2, 4.2, 4.2,
            4.4, 4.4, 4.4, 4.4, 4.6, 4.6, 4.6, 4.6, 4.6, 5,
        };

        public List<double> GetEmpericalSample()
        {
            return EmpericalSample;
        }
    }
}