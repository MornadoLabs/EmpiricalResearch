using Lab7.Services;
using Lab7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab7.Controllers
{
    public class HomeController : Controller
    {
        private static ExperimentResultsService _experimentResultsService;
        public static ExperimentResultsService ExperimentResultsService
        {
            get
            {
                if (_experimentResultsService == null)
                {
                    _experimentResultsService = new ExperimentResultsService();
                }
                return _experimentResultsService;
            }
        }

        public ActionResult Index()
        {
            var model = new OutputViewModel
            {
                Delta = ExperimentResultsService.Delta,
                DeltaAlpha = ExperimentResultsService.DeltaAlpha,
                DeltaBetha = ExperimentResultsService.DeltaBetha,
                NotDelta = ExperimentResultsService.NotDelta,
                NotDeltaAlpha = ExperimentResultsService.NotDeltaAlpha,
                NotDeltaBetha = ExperimentResultsService.NotDeltaBetha,
                Alpha = ExperimentResultsService.Alpha,
                Betha = ExperimentResultsService.Betha,
                NotAlpha = ExperimentResultsService.NotAlpha,
                NotBetha = ExperimentResultsService.NotBetha,
                Ryorx = ExperimentResultsService.Ryorx,
                Rxory = ExperimentResultsService.Rxory,
                MiddleXValue = ExperimentResultsService.MiddleXValue,
                MiddleYValue = ExperimentResultsService.MiddleYValue
            };
            ExperimentResultsService.RoundOutputViewModel(model);

            return View(model);
        }

        public JsonResult LoadData()
        {
            var model = new OutputViewModel
            {
                SqrMethodChartData = ExperimentResultsService.GetMinSqrMethodCharts(),
                CoefMethodChartData = ExperimentResultsService.GetCoefMethodCharts()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}