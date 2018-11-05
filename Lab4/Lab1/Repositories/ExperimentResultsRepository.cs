using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4.Repositories
{
    public class ExperimentResultsRepository
    {
        private double[,] ExperimentResults { get; set; } =
        {
            { 0.059, 0.057, 0.059, 0.068 },
            { 0.076, 0.078, 0.111, 0.095 },
            { 0.094, 0.053, 0.094, 0.160 },
            { 0.097, 0.118, 0.105, 0.051 },
            { 0.120, 0.122, 0.148, 0.110 },
            { 0.151, 0.137, 0.127, 0.126 }
        };

        public int M => ExperimentResults.GetLength(0);
        public int N => ExperimentResults.GetLength(1);

        public double this[int i, int j]
        {
            get
            {
                return ExperimentResults[i, j];
            }
        }
    }
}