using Lab1.Services;
using Lab1.Models;
using Lab1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab1.Controllers
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
                DiscreteRowByFrequency = EmpericalSampleService.GetDiscreteRowByFrequency(),
                DiscreteRowByVirtualFrequency = EmpericalSampleService.GetDiscreteRowByVirtualFrequency(),
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
                Sample = EmpericalSampleService.GetSample().ToChartList(),
                DiscreteRowByFrequency = EmpericalSampleService.GetDiscreteRowByFrequency().ToChartList(),
                DiscreteRowByVirtualFrequency = EmpericalSampleService.GetDiscreteRowByVirtualFrequency().ToChartList().Round(),
                PolygonFrequency = EmpericalSampleService.GetDiscreteRowByFrequency().ToChartList(),
                PolygonVirtualFrequency = EmpericalSampleService.GetDiscreteRowByVirtualFrequency().ToChartList().Round(),
                CumulusFrequency = EmpericalSampleService.GetCumulusByFrequency().ToChartList(),
                CumulusVirtualFrequency = EmpericalSampleService.GetCumulusByVirtualFrequency().ToChartList().Round(),
                EmpericalFunction = EmpericalSampleService.GetEmpericalFunction()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}