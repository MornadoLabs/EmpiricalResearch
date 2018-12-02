using Lab5.Services;
using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab5.Controllers
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
                InputTable = ExperimentResultsService.GetInputTable(),
                Q = ExperimentResultsService.Q,
                Q1 = ExperimentResultsService.Q1,
                Q2 = ExperimentResultsService.Q2,
                Q3 = ExperimentResultsService.Q3,
                S = ExperimentResultsService.S,
                S1 = ExperimentResultsService.S1,
                S2 = ExperimentResultsService.S2,
                S3 = ExperimentResultsService.S3,
                FemA = ExperimentResultsService.FemA,
                FemB = ExperimentResultsService.FemB,
                FcrA001 = ExperimentResultsService.FcrA(0.01),
                FcrB001 = ExperimentResultsService.FcrB(0.01),
                FcrA005 = ExperimentResultsService.FcrA(0.05),
                FcrB005 = ExperimentResultsService.FcrB(0.05),
            };
            ExperimentResultsService.RoundOutputViewModel(model);

            return View(model);
        }        
    }
}