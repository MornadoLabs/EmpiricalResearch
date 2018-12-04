using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab7.Repositories;
using Lab7.Models;

namespace Lab7.Services
{
    public class ExperimentResultsService
    {
        public ExperimentResultsService()
        {
            ExperimentResultsRepository = new ExperimentResultsRepository();
        }

        private ExperimentResultsRepository ExperimentResultsRepository { get; set; }
        
        public double MiddleXValue => ExperimentResultsRepository.XSample.Sum() / (double)ExperimentResultsRepository.XSample.Count;
        public double MiddleYValue => ExperimentResultsRepository.YSample.Sum() / (double)ExperimentResultsRepository.YSample.Count;

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
        
        public double rxy => Kxy / (S0x * S0y);        
        
        public double Delta
        {
            get
            {
                var sampleX = ExperimentResultsRepository.XSample;
                var result = sampleX.Count * sampleX.Sum(elem => Math.Pow(elem, 2));
                return result - Math.Pow(sampleX.Sum(), 2);
            }
        }
        public double DeltaAlpha
        {
            get
            {
                var sampleX = ExperimentResultsRepository.XSample;
                var sampleY = ExperimentResultsRepository.YSample;
                var part1 = sampleX.Sum(elem => Math.Pow(elem, 2)) * sampleY.Sum();
                var part2 = 0.0;
                for (int i = 0; i < sampleX.Count; i++)
                {
                    part2 += sampleX[i] * sampleY[i];
                }
                part2 *= sampleX.Sum();
                return part1 - part2;
            }
        }
        public double DeltaBetha
        {
            get
            {
                var sampleX = ExperimentResultsRepository.XSample;
                var sampleY = ExperimentResultsRepository.YSample;
                var part1 = 0.0;
                var part2 = sampleX.Sum() * sampleY.Sum();
                for (int i = 0; i < sampleX.Count; i++)
                {
                    part1 += sampleX[i] * sampleY[i];
                }
                part1 *= sampleX.Count;
                return part1 - part2;
            }
        }

        public double NotDelta
        {
            get
            {
                var sampleY = ExperimentResultsRepository.YSample;
                var result = sampleY.Count * sampleY.Sum(elem => Math.Pow(elem, 2));
                return result - Math.Pow(sampleY.Sum(), 2);
            }
        }
        public double NotDeltaAlpha
        {
            get
            {
                var sampleX = ExperimentResultsRepository.XSample;
                var sampleY = ExperimentResultsRepository.YSample;
                var part1 = sampleY.Sum(elem => Math.Pow(elem, 2)) * sampleX.Sum();
                var part2 = 0.0;
                for (int i = 0; i < sampleX.Count; i++)
                {
                    part2 += sampleX[i] * sampleY[i];
                }
                part2 *= sampleY.Sum();
                return part1 - part2;
            }
        }
        public double NotDeltaBetha => DeltaBetha;

        public double Alpha => DeltaAlpha / Delta;
        public double Betha => DeltaBetha / Delta;

        public double NotAlpha => NotDeltaAlpha / NotDelta;
        public double NotBetha => NotDeltaBetha / NotDelta;

        public double MinSqrMethodYX(double x) => Alpha + Betha * x;        
        public double MinSqrMethodXY(double x) => (x - NotAlpha) / NotBetha;        

        public double Ryorx => rxy * S0y / S0x;
        public double Rxory => rxy * S0x / S0y;

        public double CoefMethodYX(double x) => Ryorx * (x - MiddleXValue) + MiddleYValue;     
        public double CoefMethodXY(double x) => (x - MiddleXValue) / Rxory + MiddleYValue;     

        public ChartViewModel[][] GetMinSqrMethodCharts()
        {
            var result = new ChartViewModel[3][];
            result[0] = new ChartViewModel[2];
            result[1] = new ChartViewModel[2];
            result[2] = GetPoints();

            var minX = result[2].Min(elem => elem.x) - 1;
            var maxX = result[2].Max(elem => elem.x) + 1;
            result[0][0] = new ChartViewModel { x = minX, y = MinSqrMethodYX(minX) };
            result[0][1] = new ChartViewModel { x = maxX, y = MinSqrMethodYX(maxX) };
            result[1][0] = new ChartViewModel { x = minX, y = MinSqrMethodXY(minX) };
            result[1][1] = new ChartViewModel { x = maxX, y = MinSqrMethodXY(maxX) };

            return result;
        }

        public ChartViewModel[][] GetCoefMethodCharts()
        {
            var result = new ChartViewModel[3][];
            result[0] = new ChartViewModel[2];
            result[1] = new ChartViewModel[2];
            result[2] = GetPoints();

            var minX = result[2].Min(elem => elem.x) - 1;
            var maxX = result[2].Max(elem => elem.x) + 1;
            result[0][0] = new ChartViewModel { x = minX, y = CoefMethodYX(minX) };
            result[0][1] = new ChartViewModel { x = maxX, y = CoefMethodYX(maxX) };
            result[1][0] = new ChartViewModel { x = minX, y = CoefMethodXY(minX) };
            result[1][1] = new ChartViewModel { x = maxX, y = CoefMethodXY(maxX) };

            return result;
        }
        
        private ChartViewModel[] GetPoints()
        {
            var sampleX = ExperimentResultsRepository.XSample;
            var sampleY = ExperimentResultsRepository.YSample;
            var result = new ChartViewModel[sampleX.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new ChartViewModel { x = sampleX[i], y = sampleY[i] };
            }
            return result;
        }

        public void RoundOutputViewModel(OutputViewModel outputViewModel)
        {
            outputViewModel.Delta = Math.Round(outputViewModel.Delta, 4);
            outputViewModel.DeltaAlpha = Math.Round(outputViewModel.DeltaAlpha, 4);
            outputViewModel.DeltaBetha = Math.Round(outputViewModel.DeltaBetha, 4);

            outputViewModel.NotDelta = Math.Round(outputViewModel.NotDelta, 4);
            outputViewModel.NotDeltaAlpha = Math.Round(outputViewModel.NotDeltaAlpha, 4);
            outputViewModel.NotDeltaBetha = Math.Round(outputViewModel.NotDeltaBetha, 4);

            outputViewModel.Alpha = Math.Round(outputViewModel.Alpha, 4);
            outputViewModel.Betha = Math.Round(outputViewModel.Betha, 4);

            outputViewModel.NotAlpha = Math.Round(outputViewModel.NotAlpha, 4);            
            outputViewModel.NotBetha = Math.Round(outputViewModel.NotBetha, 4);

            outputViewModel.Ryorx = Math.Round(outputViewModel.Ryorx, 4);
            outputViewModel.Rxory = Math.Round(outputViewModel.Rxory, 4);

            outputViewModel.MiddleXValue = Math.Round(outputViewModel.MiddleXValue, 4);
            outputViewModel.MiddleYValue = Math.Round(outputViewModel.MiddleYValue, 4);
        }
    }
}