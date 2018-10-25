using Lab2.Services;
using Lab2.Models;
using Lab2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class HomeController : Controller
    {
        private static EmpericalSampleService empericalSampleService;
        public static EmpericalSampleService EmpericalSampleService
        {
            get
            {
                if (empericalSampleService == null)
                {
                    empericalSampleService = new EmpericalSampleService();
                }
                return empericalSampleService;
            }
        }

        public ActionResult Index()
        {
            var model = new EmpericalSampleViewModel
            {
                IsVirtual = false,
                IntervalRowByFrequency = EmpericalSampleService.GetIntervalRowByFrequency(),
                IntervalRowByVirtualFrequency = EmpericalSampleService.GetIntervalRowByVirtualFrequency(),
                MiddleEmpericalValue = Math.Round(EmpericalSampleService.GetMiddleEmpericalValue(), 2),
                Mode = EmpericalSampleService.GetMode(),
                Median = Math.Round(EmpericalSampleService.GetMedian(), 2),
                Dispersion = Math.Round(EmpericalSampleService.GetDispersion(), 2),
                MiddleStandardDeviation = Math.Round(EmpericalSampleService.GetMiddleStandardDeviation(), 2),
                Swing = Math.Round(EmpericalSampleService.GetSwing(), 2),
                VariationCoef = Math.Round(EmpericalSampleService.GetVariationCoef(), 2),
                AsymmetricalCoef = Math.Round(EmpericalSampleService.GetAsymmetricalCoef(), 2),
                Excess = Math.Round(EmpericalSampleService.GetExcess(), 2)
            };

            return View(model);
        }

        public JsonResult LoadData()
        {
            return Json(new ChartsOutputViewModel
            {
                DiscreteRowByFrequency = EmpericalSampleService.GetDiscreteRowByFrequency().ToTableList().Round(),
                IntervalRowByFrequency = EmpericalSampleService.GetIntervalRowByFrequency().ToTableList().Round(),
                IntervalRowByVirtualFrequency = EmpericalSampleService.GetIntervalRowByVirtualFrequency().ToTableList().Round(),
                HistogramByFrequency = EmpericalSampleService.GetHistogramByFrequency().Round(),
                HistogramByVirtualFrequency = EmpericalSampleService.GetHistogramByVirtualFrequency().Round(),
                CumulusFrequency = EmpericalSampleService.GetCumulusByFrequency().ToChartList(),
                CumulusVirtualFrequency = EmpericalSampleService.GetCumulusByVirtualFrequency().ToChartList().Round(),
                EmpericalFunction = EmpericalSampleService.GetEmpericalFunction()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}