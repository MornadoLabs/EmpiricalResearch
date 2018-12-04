using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab6.Repositories
{
    public class ExperimentResultsRepository
    {
        private List<List<double>> BigExperimentResults { get; set; } = new List<List<double>>
        {
            new List<double>{ 0.37, 0.37, 0.30, 0.38, 0.33, 0.22, 0.28, 0.26, 0.3, 0.29, 0.32, 0.36, 0.37, 0.37, 0.42, 0.4, 0.26, 0.34, 0.57, 0.72, 0.77, 0.85, 0.64, 0.45, 0.24, 0.24, 0.32, 0.35, 0.43, 0.62, 0.81, 0.99, 0.59, 0.43, 0.23, 0.15, 0.17, 0.3, 0.43, 0.65, 0.6, 0.43, 0.34, 0.1, 0.0, 0.02, 0.22, 0.29, 0.31, 0.36, 0.63, 0.76, 0.56, 0.32, 0.13 },
            new List<double>{ 0.38, 0.37, 0.38, 0.36, 0.4, 0.36, 0.48, 0.32, 0.32, 0.31, 0.36, 0.38, 0.4, 0.42, 0.43, 0.41, 0.29, 0.34, 0.49, 0.6, 0.86, 0.99, 0.72, 0.43, 0.25, 0.24, 0.32, 0.34, 0.41, 0.57, 0.81, 0.97, 0.59, 0.36, 0.21, 0.25, 0.32, 0.36, 0.44, 0.65, 0.88, 0.57, 0.42, 0.25, 0.0, 0.03, 0.24, 0.33, 0.33, 0.38, 0.63, 0.79, 0.55, 0.34, 0.14 },
        };

        private List<List<double>> ExperimentResults { get; set; } = new List<List<double>>
        {
            new List<double>{ 0.37, 0.37, 0.30, 0.38, 0.33, 0.22, 0.28, 0.26, 0.26, 0.34, 0.57, 0.72, 0.77, 0.85, 0.64, 0.45, 0.24, 0.24, 0.32, 0.35, 0.43, 0.62, 0.81, 0.99, 0.59, 0.43, 0.23, 0.15, 0.17, 0.3, 0.43, 0.65, 0.6, 0.43, 0.34, 0.1, 0.0 },
            new List<double>{ 0.38, 0.37, 0.38, 0.36, 0.4, 0.36, 0.48, 0.32, 0.29, 0.34, 0.49, 0.6, 0.86, 0.99, 0.72, 0.43, 0.25, 0.24, 0.32, 0.34, 0.41, 0.57, 0.81, 0.97, 0.59, 0.36, 0.21, 0.25, 0.32, 0.36, 0.44, 0.65, 0.88, 0.57, 0.42, 0.25, 0.0 },
        };


        public List<double> BigXSample => BigExperimentResults[0];
        public List<double> BigYSample => BigExperimentResults[1];

        public List<double> XSample => ExperimentResults[0];
        public List<double> YSample => ExperimentResults[1];
    }
}