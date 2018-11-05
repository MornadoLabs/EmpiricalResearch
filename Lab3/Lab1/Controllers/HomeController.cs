using Lab3.Services;
using Lab3.Models;
using Lab3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab3.Controllers
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
                Mark001 = new MarkViewModel
                {
                    Importance = 0.001,
                    IntervalMarkForKnownDispersion = EmpericalSampleService.GetIntervalMarkForKnownDispersion(0.001),
                    IntervalMarkForUnknownDispersion = EmpericalSampleService.GetIntervalMarkForUnknownDispersion(0.001),
                    IntervalMarkMiddleStandardDeviation = EmpericalSampleService.GetIntervalMarkMiddleStandardDeviation(0.001)
                },

                Mark01 = new MarkViewModel
                {
                    Importance = 0.01,
                    IntervalMarkForKnownDispersion = EmpericalSampleService.GetIntervalMarkForKnownDispersion(0.01),
                    IntervalMarkForUnknownDispersion = EmpericalSampleService.GetIntervalMarkForUnknownDispersion(0.01),
                    IntervalMarkMiddleStandardDeviation = EmpericalSampleService.GetIntervalMarkMiddleStandardDeviation(0.01)
                },

                Mark05 = new MarkViewModel
                {
                    Importance = 0.05,
                    IntervalMarkForKnownDispersion = EmpericalSampleService.GetIntervalMarkForKnownDispersion(0.05),
                    IntervalMarkForUnknownDispersion = EmpericalSampleService.GetIntervalMarkForUnknownDispersion(0.05),
                    IntervalMarkMiddleStandardDeviation = EmpericalSampleService.GetIntervalMarkMiddleStandardDeviation(0.05)
                },
            };

            return View(model);
        }

        public JsonResult LoadData()
        {
            return Json(new ChartsOutputViewModel
            {
                Sample = EmpericalSampleService.GetSample().ToChartList(),               
            }, JsonRequestBehavior.AllowGet);
        }
    }
}