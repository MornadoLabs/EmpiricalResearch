using Lab1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1.Services
{
    public class EmpericalSampleService
    {
        public EmpericalSampleService()
        {
            this.EmpericalSampleRepository = new EmpericalSampleRepository();        
        }

        private EmpericalSampleRepository EmpericalSampleRepository { get; set; }
        

        public Dictionary<double, int> GetDiscreteRowByFrequency()
        {
            var result = new Dictionary<double, int>();
            var elements = this.GetSampleElements();

            foreach (var element in elements)
            {
                result.Add(element, GetElementFrequency(element));
            }

            return result;
        }

        public Dictionary<double, double> GetDiscreteRowByVirtualFrequency()
        {
            var result = new Dictionary<double, double>();
            var elements = this.GetSampleElements();

            foreach (var element in elements)
            {
                result.Add(element, GetElementVirtualFrequency(element));
            }

            return result;
        }

        public double GetMiddleEmpericalValue()
        {
            return GetStartEmpericalMoment(1);
        }

        public double GetDeviation(double element)
        {
            var discreteRow = this.GetDiscreteRowByFrequency();
            var middleEmpericalValue = this.GetMiddleEmpericalValue();
            return (element - middleEmpericalValue) * discreteRow[element];
        }

        public List<double> GetMode()
        {
            var discreteRow = GetDiscreteRowByFrequency();
            var modeValue = discreteRow.Values.Max();
            return discreteRow.Where(pair => pair.Value == modeValue).Select(p => p.Key).ToList();
        }

        public double GetMedian()
        {
            var sample = EmpericalSampleRepository.GetEmpericalSample();
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
            var elements = GetSampleElements();
            return elements.Max() - elements.Min();
        }

        public double GetVariationCoef()
        {
            return GetMiddleStandardDeviation() / GetMiddleEmpericalValue() * 100;
        }

        public double GetStartEmpericalMoment(int k)
        {
            var discreteRow = this.GetDiscreteRowByFrequency();
            var result = 0.0;

            foreach (var element in discreteRow.Keys)
            {
                result += Math.Pow(element, k) * discreteRow[element];
            }

            return result / EmpericalSampleRepository.GetEmpericalSample().Count;
        }

        public double GetMiddleEmpericalMoment(int k)
        {
            var discreteRow = this.GetDiscreteRowByFrequency();
            var result = 0.0;
            var middleEmpericalValue = GetMiddleEmpericalValue();

            foreach (var element in discreteRow.Keys)
            {
                result += Math.Pow(element - middleEmpericalValue, k) * discreteRow[element];
            }

            return result / EmpericalSampleRepository.GetEmpericalSample().Count;
        }

        public double GetAsymmetricalCoef()
        {
            return GetMiddleEmpericalMoment(3) / GetMiddleStandardDeviation();
        }

        public double GetExcess()
        {
            return GetMiddleEmpericalMoment(4) / Math.Pow(GetMiddleStandardDeviation(), 4) - 3;
        }

        
        public List<double> GetSample()
        {
            return this.EmpericalSampleRepository.GetEmpericalSample();
        }

        private List<double> GetSampleElements()
        {
            return this.EmpericalSampleRepository.GetEmpericalSample().Distinct().ToList();
        }

        private int GetElementFrequency(double element)
        {
            return EmpericalSampleRepository.GetEmpericalSample().Count(elem => elem == element);
        }

        private double GetElementVirtualFrequency(double element)
        {
            return
                EmpericalSampleRepository.GetEmpericalSample().Count(elem => elem == element) /
                EmpericalSampleRepository.GetEmpericalSample().Count();
        }
    }
}