using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab6.Repositories;
using Lab6.Models;

namespace Lab6.Services
{
    public class ExperimentResultsService
    {
        public ExperimentResultsService()
        {
            ExperimentResultsRepository = new ExperimentResultsRepository();
        }

        private ExperimentResultsRepository ExperimentResultsRepository { get; set; }
        
        private double MiddleBigXValue => ExperimentResultsRepository.BigXSample.Sum() / (double)ExperimentResultsRepository.BigXSample.Count;
        private double MiddleBigYValue => ExperimentResultsRepository.BigYSample.Sum() / (double)ExperimentResultsRepository.BigYSample.Count;

        private double MiddleXValue => ExperimentResultsRepository.XSample.Sum() / (double)ExperimentResultsRepository.XSample.Count;
        private double MiddleYValue => ExperimentResultsRepository.YSample.Sum() / (double)ExperimentResultsRepository.YSample.Count;

        public double Kxy
        {
            get
            {
                double result = 0, middleX = MiddleXValue, middleY = MiddleYValue;
                var sampleX = ExperimentResultsRepository.XSample;
                var sampleY = ExperimentResultsRepository.YSample;
                for (int i = 0; i < sampleX.Count; i++)
                {
                    result += (sampleX[i] - middleX) * (sampleY[i] - middleY);
                }
                return result / (sampleX.Count - 1);
            }
        }

        public double BigKxy
        {
            get
            {
                double result = 0, middleX = MiddleBigXValue, middleY = MiddleBigYValue;
                var sampleX = ExperimentResultsRepository.BigXSample;
                var sampleY = ExperimentResultsRepository.BigYSample;
                for (int i = 0; i < sampleX.Count; i++)
                {
                    result += (sampleX[i] - middleX) * (sampleY[i] - middleY);
                }
                return result / (sampleX.Count - 1);
            }
        }
        
        public double S0x
        {
            get
            {
                double result = 0, middleX = MiddleXValue;
                var sampleX = ExperimentResultsRepository.XSample;
                for (int i = 0; i < sampleX.Count; i++)
                {
                    result += Math.Pow((sampleX[i] - middleX), 2);
                }
                return Math.Sqrt(result / (sampleX.Count - 1));
            }
        }
        public double S0y
        {
            get
            {
                double result = 0, middleY = MiddleYValue;
                var sampleY = ExperimentResultsRepository.YSample;
                for (int i = 0; i < sampleY.Count; i++)
                {
                    result += Math.Pow((sampleY[i] - middleY), 2);
                }
                return Math.Sqrt(result / (sampleY.Count - 1));
            }
        }

        public double BigS0x
        {
            get
            {
                double result = 0, middleX = MiddleBigXValue;
                var sampleX = ExperimentResultsRepository.BigXSample;
                for (int i = 0; i < sampleX.Count; i++)
                {
                    result += Math.Pow((sampleX[i] - middleX), 2);
                }
                return Math.Sqrt(result / (sampleX.Count - 1));
            }
        }
        public double BigS0y
        {
            get
            {
                double result = 0, middleY = MiddleBigYValue;
                var sampleY = ExperimentResultsRepository.BigYSample;
                for (int i = 0; i < sampleY.Count; i++)
                {
                    result += Math.Pow((sampleY[i] - middleY), 2);
                }
                return Math.Sqrt(result / (sampleY.Count - 1));
            }
        }

        public double rxy => Kxy / (S0x * S0y);
        public double BigRxy => BigKxy / (BigS0x * BigS0y);

        public double RomCoef => 3 * (1 - BigRxy * BigRxy) / Math.Sqrt(ExperimentResultsRepository.BigXSample.Count);

        public double Z => Math.Log((1 + rxy) / (1 - rxy)) / 2;

        private double GetT(double alpha)
        {
            return alpha == 0.01 ? 2.58 : 1.96;
        }

        public double GetR1xy(double alpha) => Z - (GetT(alpha) / Math.Sqrt(ExperimentResultsRepository.XSample.Count - 3));
        public double GetR2xy(double alpha) => Z + (GetT(alpha) / Math.Sqrt(ExperimentResultsRepository.XSample.Count - 3));

        public void RoundOutputViewModel(OutputViewModel outputViewModel)
        {
            outputViewModel.Z = Math.Round(outputViewModel.Z, 4);
            outputViewModel.Kxy = Math.Round(outputViewModel.Kxy, 4);
            outputViewModel.S0x = Math.Round(outputViewModel.S0x, 4);
            outputViewModel.S0y = Math.Round(outputViewModel.S0y, 4);

            outputViewModel.Rxy = Math.Round(outputViewModel.Rxy, 4);
            outputViewModel.R1xy001 = Math.Round(outputViewModel.R1xy001, 4);
            outputViewModel.R2xy001 = Math.Round(outputViewModel.R2xy001, 4);
            outputViewModel.R1xy005 = Math.Round(outputViewModel.R1xy005, 4);
            outputViewModel.R2xy005 = Math.Round(outputViewModel.R2xy005, 4);
            
            outputViewModel.BigKxy = Math.Round(outputViewModel.BigKxy, 4);
            outputViewModel.BigS0x = Math.Round(outputViewModel.BigS0x, 4);
            outputViewModel.BigS0y = Math.Round(outputViewModel.BigS0y, 4);
            outputViewModel.BigRxy = Math.Round(outputViewModel.BigRxy, 4);
            outputViewModel.RomCoef = Math.Round(outputViewModel.RomCoef, 4);
        }
    }
}