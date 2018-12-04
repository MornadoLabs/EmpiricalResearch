using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab7.Repositories
{
    public class ExperimentResultsRepository
    {
        private List<List<double>> ExperimentResults { get; set; } = new List<List<double>>
        {
            new List<double>{ 0.3, 0.29, 0.32, 0.36, 0.37, 0.37, 0.42, 0.4, 0.24, 0.32, 0.35, 0.43, 0.62, 0.81, 0.99, 0.59, 0.43, 0.23, },
            new List<double>{ 0.32, 0.31, 0.36, 0.38, 0.4, 0.42, 0.43, 0.41, 0.24, 0.32, 0.34, 0.41, 0.57, 0.81, 0.97, 0.59, 0.36, 0.21, },
        };        

        public List<double> XSample => ExperimentResults[0];
        public List<double> YSample => ExperimentResults[1];
    }
}