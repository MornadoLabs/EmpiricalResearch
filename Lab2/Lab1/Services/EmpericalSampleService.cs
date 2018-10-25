using Lab2.Models;
using Lab2.Helpers;
using Lab2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Services
{
    public class EmpericalSampleService
    {
        public EmpericalSampleService()
        {
            this.EmpericalSampleRepository = new EmpericalSampleRepository();        
        }

        private EmpericalSampleRepository EmpericalSampleRepository { get; set; }

        private double IntervalSize
        {
            get
            {
                var sample = this.EmpericalSampleRepository.GetGeneralSample();
                return (sample.Max() - sample.Min()) / (1 + 3.322 * Math.Log(sample.Count));
            }
        }

#region Rows Getters

        public Dictionary<string, double> GetDiscreteRowByFrequency()
        {
            var result = new Dictionary<string, double>();
            var elements = this.GetIntervalEmpericalSample();

            foreach (var element in elements)
            {
                result.Add(
                    elements.IndexOf(element) == elements.Count - 1
                    ? element.Name.Replace(")", "]")
                    : element.Name, 
                    element.DiscreteValue);
            }
            
            return result;
        }

        public Dictionary<string, double> GetIntervalRowByFrequency()
        {
            var result = new Dictionary<string, double>();
            var intervals = this.GetIntervalEmpericalSample();

            foreach (var element in intervals)
            {
                result.Add(
                    intervals.IndexOf(element) == intervals.Count - 1
                    ? element.Name.Replace(")", "]")
                    : element.Name, 
                    element.IntervalData.Count);
            }
            
            return result;
        }

        public Dictionary<string, double> GetIntervalRowByVirtualFrequency()
        {
            var result = new Dictionary<string, double>();
            var intervals = this.GetIntervalEmpericalSample();

            foreach (var element in intervals)
            {
                result.Add(
                    intervals.IndexOf(element) == intervals.Count - 1
                    ? element.Name.Replace(")", "]")
                    : element.Name,
                    (double)element.IntervalData.Count / EmpericalSampleRepository.GetGeneralSample().Count);
            }
            
            return result;
        }

        #endregion

#region Sample Parameters Getters

        public double GetMiddleEmpericalValue()
        {
            return GetStartEmpericalMoment(1);
        }
        
        public List<double> GetMode()
        {
            var sampleFrequencies = EmpericalSampleRepository.GetGeneralSample()
                                .GroupBy(elem => elem)
                                .Select(group => new { Key = group.Key, Frequency = group.Count() })
                                .ToList();
            var sampleMode = sampleFrequencies.Where(elem => elem.Frequency == sampleFrequencies.Max(el => el.Frequency)).Select(x => x.Key).FirstOrDefault();

            var intervals = GetIntervalEmpericalSample();
            var modeIntervals = intervals.Where(interval => interval.HasElement(sampleMode)).ToList();
            var result = new List<double>();

            foreach (var modeInterval in modeIntervals)
            {
            var preModeInterval = intervals.Where(interval => interval.IntervalEnd == modeInterval.IntervalStart).FirstOrDefault();
            var nextModeInterval = intervals.Where(interval => interval.IntervalStart == modeInterval.IntervalEnd).FirstOrDefault();
                result.Add(Math.Round(
                        modeInterval.IntervalStart
                        + (double)(modeInterval.IntervalData.Count - preModeInterval.IntervalData.Count)
                        / (2 * modeInterval.IntervalData.Count - preModeInterval.IntervalData.Count - nextModeInterval.IntervalData.Count)
                        * IntervalSize
                    , 2));
            }
            
            return result;
        }

        public double GetMedian()
        {
            var sampleMedian = GetSampleMedian();
            var intervals = GetIntervalEmpericalSample();
            var medianInterval = intervals.FirstOrDefault(elem => elem.HasElement(sampleMedian));
            var preMedianIntervals = intervals.Where(elem => elem.IntervalStart < medianInterval.IntervalStart).ToList();

            return medianInterval.IntervalStart
                + (IntervalSize / medianInterval.IntervalData.Count)
                * ((double) EmpericalSampleRepository.GetGeneralSample().Count / 2 - preMedianIntervals.Sum(x => x.IntervalData.Count))
                * IntervalSize;
        }

        public double GetDispersion()
        {
            return GetMiddleEmpericalMoment(2);
        }

        public double GetMiddleStandardDeviation()
        {
            return Math.Sqrt(GetDispersion());
        }

        public double GetSwing()
        {
            var elements = GetSample();
            return elements.Max() - elements.Min();
        }

        public double GetVariationCoef()
        {
            return GetMiddleStandardDeviation() / GetMiddleEmpericalValue() * 100;
        }

        public double GetStartEmpericalMoment(int k)
        {
            var intervals = GetIntervalEmpericalSample();
            var result = 0.0;

            foreach (var interval in intervals)
            {
                result += Math.Pow(interval.DiscreteValue, k) * interval.IntervalData.Count;
            }

            return result / EmpericalSampleRepository.GetGeneralSample().Count;
        }

        public double GetMiddleEmpericalMoment(int k)
        {
            var intervals = GetIntervalEmpericalSample();
            var result = 0.0;
            var middleEmpericalValue = GetMiddleEmpericalValue();

            foreach (var interval in intervals)
            {
                result += Math.Pow(interval.DiscreteValue - middleEmpericalValue, k) * interval.IntervalData.Count;
            }

            return result / EmpericalSampleRepository.GetGeneralSample().Count;
        }

        public double GetAsymmetricalCoef()
        {
            return GetMiddleEmpericalMoment(3) / GetMiddleStandardDeviation();
        }

        public double GetExcess()
        {
            return GetMiddleEmpericalMoment(4) / Math.Pow(GetMiddleStandardDeviation(), 4) - 3;
        }

#endregion


        public List<double> GetCumulusByFrequency()
        {
            var sample = this.GetIntervalRowByFrequency();

            if (sample.Count < 1)
            {
                return null;
            }

            var result = new List<double> { sample[sample.Keys.ToList()[0]] };

            for (int i = 1; i < sample.Keys.Count; i++)
            {
                result.Add(result[i - 1] + sample[sample.Keys.ToList()[i]]);
            }

            return result;
        }

        public List<double> GetCumulusByVirtualFrequency()
        {
            var sample = this.GetIntervalRowByVirtualFrequency();

            if (sample.Count < 1)
            {
                return null;
            }

            var result = new List<double> { sample[sample.Keys.ToList()[0]] };

            for (int i = 1; i < sample.Count; i++)
            {
                result.Add(result[i - 1] + sample[sample.Keys.ToList()[i]]);
            }

            return result;
        }

        public List<ChartViewModel> GetEmpericalFunction()
        {
            var intervals = GetIntervalEmpericalSample();
            var sampleSize = EmpericalSampleRepository.GetGeneralSample().Count;
            var dictionaryOfElements = new Dictionary<double, double>();

            foreach (var interval in intervals)
            {
                dictionaryOfElements.Add(interval.DiscreteValue, intervals.Where(elem => elem.IntervalStart < interval.IntervalStart).Sum(elem => elem.IntervalData.Count) / (double)sampleSize);
            }
            var result = dictionaryOfElements.ToChartList().Round();
            result.Add(new ChartViewModel { x = intervals.Max(x => x.DiscreteValue), y = 1 });

            return result;
        }

        public List<TableViewModel> GetHistogramByFrequency()
        {
            var intervals = GetIntervalEmpericalSample();
            var result = new List<TableViewModel>();

            foreach (var interval in intervals)
            {
                result.Add(new TableViewModel
                {
                    x = intervals.IndexOf(interval) == intervals.Count - 1
                        ? interval.Name.Replace(")", "]")
                        : interval.Name,
                    y = interval.IntervalData.Count / IntervalSize
                });
            }

            return result;
        }

        public List<TableViewModel> GetHistogramByVirtualFrequency()
        {
            var intervals = GetIntervalEmpericalSample();
            var result = new List<TableViewModel>();

            foreach (var interval in intervals)
            {
                result.Add(new TableViewModel
                {
                    x = intervals.IndexOf(interval) == intervals.Count - 1
                        ? interval.Name.Replace(")", "]")
                        : interval.Name,
                    y = interval.IntervalData.Count 
                        / (double)EmpericalSampleRepository.GetGeneralSample().Count
                        / IntervalSize
                });
            }

            return result;
        }



        public List<double> GetSample()
        {
            return GetIntervalEmpericalSample().Select(k => k.DiscreteValue).ToList();
        }        

        private List<IntervalModel> GetIntervalEmpericalSample()
        {
            var sample = this.EmpericalSampleRepository.GetGeneralSample();
            var result = new List<IntervalModel>();
            var intervalSize = IntervalSize;

            for (double i = sample.Min(); i <= sample.Max(); i += intervalSize)
            {
                var intervalEnd = i + intervalSize;
                var intervalData = sample.Where(
                    elem => intervalEnd >= sample.Max() 
                            ? elem >= i 
                            : elem >= i && elem < intervalEnd).ToList();

                result.Add(new IntervalModel { IntervalStart = i, IntervalEnd = intervalEnd >= sample.Max() ? sample.Max() : intervalEnd, IntervalData = intervalData });
            }

            return result;
        }

        private double GetSampleMedian()
        {
            var sample = EmpericalSampleRepository.GetGeneralSample();
            sample.Sort();

            if (sample.Count % 2 == 0)
            {
                var middleId = sample.Count / 2;
                return (sample[middleId] + sample[middleId - 1]) / 2;
            }
            else
            {
                return sample[sample.Count / 2];
            }
        }
    }
}