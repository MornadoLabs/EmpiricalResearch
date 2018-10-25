using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Services.Tests
{
    [TestClass()]
    public class EmpericalSampleServiceTests
    {
        private static List<double> TestInput = new List<double> { 1, 2, 4, 3, 2, 5 };

        [TestMethod()]
        public void GetDiscreteRowByFrequencyTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var expectedResult = new Dictionary<double, int> { { 1, 1 }, { 2, 2 }, { 3, 1 }, { 4, 1 }, { 5, 1 } };
            var actualResult = service.GetDiscreteRowByFrequency();

            Assert.IsTrue(actualResult.Keys.ToList().TrueForAll(key => actualResult[key] == expectedResult[key]));
        }

        [TestMethod()]
        public void GetDiscreteRowByVirtualFrequencyTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var expectedResult = new Dictionary<double, double>
            {
                { 1, 1 / 6.0 },
                { 2, 1 / 3.0 },
                { 3, 1 / 6.0 },
                { 4, 1 / 6.0 },
                { 5, 1 / 6.0 }
            };
            var actualResult = service.GetDiscreteRowByVirtualFrequency();

            Assert.IsTrue(actualResult.Keys.ToList().TrueForAll(key => actualResult[key] == expectedResult[key]));            
        }

        [TestMethod()]
        public void GetMiddleEmpericalValueTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var expectedResult = 0.0;
            foreach(var elem in TestInput.Distinct().ToList())
            {
                expectedResult += elem * TestInput.Count(x => x == elem);
            }
            expectedResult /= TestInput.Count;
            
            Assert.AreEqual(expectedResult, service.GetMiddleEmpericalValue());
        }

        [TestMethod()]
        public void GetDeviationTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);
            
            var middleEmpericalValue = 0.0;
            foreach(var elem in TestInput.Distinct().ToList())
            {
                middleEmpericalValue += elem * TestInput.Count(x => x == elem);
            }
            middleEmpericalValue /= TestInput.Count;

            var expectedResult = new Dictionary<double, double>
            {
                { 1, 1 - middleEmpericalValue },
                { 2, (2 - middleEmpericalValue) * 2 },
                { 3, 3 - middleEmpericalValue },
                { 4, 4 - middleEmpericalValue },
                { 5, 5 - middleEmpericalValue }
            };
            
            Assert.IsTrue(expectedResult.Keys.ToList().TrueForAll(elem => expectedResult[elem] == service.GetDeviation(elem)));
        }

        [TestMethod()]
        public void GetModeTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);
            Assert.AreEqual(2, service.GetMode()[0]);
        }

        [TestMethod()]
        public void GetMedianTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);
            Assert.AreEqual(2.5, service.GetMedian());
        }

        [TestMethod()]
        public void GetDispersionTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var middleEmpericalValue = 0.0;
            var expectedResult = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                middleEmpericalValue += elem * elemFrequency;
                expectedResult += Math.Pow(elem, 2) * elemFrequency;
            }
            middleEmpericalValue /= TestInput.Count;
            expectedResult /= TestInput.Count;

            expectedResult -= Math.Pow(middleEmpericalValue, 2);

            Assert.AreEqual(expectedResult, service.GetDispersion());
        }

        [TestMethod()]
        public void GetMiddleStandardDeviationTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var middleEmpericalValue = 0.0;
            var expectedResult = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                middleEmpericalValue += elem * elemFrequency;
                expectedResult += Math.Pow(elem, 2) * elemFrequency;
            }
            middleEmpericalValue /= TestInput.Count;
            expectedResult /= TestInput.Count;

            expectedResult -= Math.Pow(middleEmpericalValue, 2);
            expectedResult = Math.Pow(expectedResult, 0.5);

            Assert.AreEqual(expectedResult, service.GetMiddleStandardDeviation());
        }

        [TestMethod()]
        public void GetSwingTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var expectedResult = TestInput.Max() - TestInput.Min();

            Assert.AreEqual(expectedResult, service.GetSwing());
        }

        [TestMethod()]
        public void GetVariationCoefTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var middleEmpericalValue = 0.0;
            var middleStandartDeviation = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                middleEmpericalValue += elem * elemFrequency;
                middleStandartDeviation += Math.Pow(elem, 2) * elemFrequency;
            }
            middleEmpericalValue /= TestInput.Count;
            middleStandartDeviation /= TestInput.Count;

            middleStandartDeviation -= Math.Pow(middleEmpericalValue, 2);
            middleStandartDeviation = Math.Pow(middleStandartDeviation, 0.5);

            var expectedResult = middleStandartDeviation / middleEmpericalValue * 100;

            Assert.AreEqual(expectedResult, service.GetVariationCoef());
        }

        [TestMethod()]
        public void GetStartEmpericalMomentTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var empericalMomentNumber = 4;
            var expectedResult = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                expectedResult += Math.Pow(elem, empericalMomentNumber) * TestInput.Count(x => x == elem);
            }
            expectedResult /= TestInput.Count;

            Assert.AreEqual(expectedResult, service.GetStartEmpericalMoment(empericalMomentNumber));
        }

        [TestMethod()]
        public void GetMiddleEmpericalMomentTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var middleEmpericalMomentNumber = 5;
            var middleEmpericalValue = 0.0;
            var expectedResult = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                middleEmpericalValue += elem * elemFrequency;                
            }
            middleEmpericalValue /= TestInput.Count;

            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                expectedResult += Math.Pow(elem - middleEmpericalValue, middleEmpericalMomentNumber) * elemFrequency;      
            }
            expectedResult /= TestInput.Count;

            Assert.AreEqual(expectedResult, service.GetMiddleEmpericalMoment(middleEmpericalMomentNumber));
        }

        [TestMethod()]
        public void GetAsymmetricalCoefTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var middleEmpericalMomentNumber = 3;
            var middleEmpericalValue = 0.0;
            var middleStandardDeviation = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                middleEmpericalValue += elem * elemFrequency;
                middleStandardDeviation += Math.Pow(elem, 2) * elemFrequency;
            }
            middleEmpericalValue /= TestInput.Count;
            middleStandardDeviation /= TestInput.Count;

            middleStandardDeviation -= Math.Pow(middleEmpericalValue, 2);
            middleStandardDeviation = Math.Pow(middleStandardDeviation, 0.5);


            var middleEmpericalMoment = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                middleEmpericalMoment += Math.Pow(elem - middleEmpericalValue, middleEmpericalMomentNumber) * elemFrequency;
            }
            middleEmpericalMoment /= TestInput.Count;

            var expectedResult = middleEmpericalMoment / middleStandardDeviation;

            Assert.AreEqual(expectedResult, service.GetAsymmetricalCoef());
        }

        [TestMethod()]
        public void GetExcessTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var middleEmpericalMomentNumber = 4;
            var middleEmpericalValue = 0.0;
            var middleStandardDeviation = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                middleEmpericalValue += elem * elemFrequency;
                middleStandardDeviation += Math.Pow(elem, 2) * elemFrequency;
            }
            middleEmpericalValue /= TestInput.Count;
            middleStandardDeviation /= TestInput.Count;

            middleStandardDeviation -= Math.Pow(middleEmpericalValue, 2);
            middleStandardDeviation = Math.Pow(middleStandardDeviation, 0.5);


            var middleEmpericalMoment = 0.0;
            foreach (var elem in TestInput.Distinct().ToList())
            {
                var elemFrequency = TestInput.Count(x => x == elem);
                middleEmpericalMoment += Math.Pow(elem - middleEmpericalValue, middleEmpericalMomentNumber) * elemFrequency;
            }
            middleEmpericalMoment /= TestInput.Count;

            var expectedResult = middleEmpericalMoment / Math.Pow(middleStandardDeviation , 4) - 3;

            Assert.AreEqual(expectedResult, service.GetExcess());
        }

        [TestMethod()]
        public void GetCumulusByFrequencyTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var testElements = TestInput.Distinct().ToList();
            var expectedResult = new List<double> { TestInput.Count(elem => elem == testElements[0]) };
            for (int i = 1; i < testElements.Count; i++)
            {
                expectedResult.Add(expectedResult[i - 1] + TestInput.Count(elem => elem == testElements[i]));
            }

            Assert.IsTrue(expectedResult.TrueForAll(elem => expectedResult[expectedResult.IndexOf(elem)] == 
                                                                service.GetCumulusByFrequency()[expectedResult.IndexOf(elem)]));
        }

        [TestMethod()]
        public void GetCumulusByVirtualFrequencyTest()
        {
            var service = new EmpericalSampleService();
            service.SetEmpericalSample(TestInput);

            var testElements = TestInput.Distinct().ToList();
            var expectedResult = new List<double> { TestInput.Count(elem => elem == testElements[0]) / TestInput.Count };
            for (int i = 1; i < testElements.Count; i++)
            {
                expectedResult.Add(expectedResult[i - 1] + TestInput.Count(elem => elem == testElements[i]) / TestInput.Count);
            }

            Assert.IsTrue(expectedResult.TrueForAll(elem => expectedResult[expectedResult.IndexOf(elem)] ==
                                                                service.GetCumulusByFrequency()[expectedResult.IndexOf(elem)]));
        }
        
        [TestMethod()]
        public void GetSampleTest()
        {
            var service = new EmpericalSampleService();
            var inputSample = TestInput;

            service.SetEmpericalSample(inputSample);
            var serviceSample = service.GetSample();

            Assert.IsInstanceOfType(serviceSample, typeof(List<double>));
            Assert.IsTrue(serviceSample.TrueForAll(elem => serviceSample.IndexOf(elem) == inputSample.IndexOf(elem)));
        }
    }
}