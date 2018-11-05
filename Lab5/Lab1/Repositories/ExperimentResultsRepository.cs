using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5.Repositories
{
    public class ExperimentResultsRepository
    {
        private double[,] ExperimentResults { get; set; } =
        {
            { 0.164, 0.145, 0.122, 0.135 },
            { 0.275, 0.203, 0.187, 0.127 },
            { 0.276, 0.096, 0.183, 0.185 },
            { -0.101, 0.122, 0.105, 0.058 },
            { -0.107, 0.152, 0.116, 0.000 },
            { -0.013, 0.130, 0.098, 0.084 },
            { -0.097, 0.094, 0.094, 0.062 },
            { -0.021, 0.122, 0.128, 0.096 },
            { -0.086, 0.098, 0.075, 0.024 },
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