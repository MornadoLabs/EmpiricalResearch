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
                S0 = ExperimentResultsService.S0,
                S1 = ExperimentResultsService.S1,
                S2 = ExperimentResultsService.S2,
                Fem = ExperimentResultsService.Fem,
                Fcr = ExperimentResultsService.Fcr
            };
            ExperimentResultsService.RoundOutputViewModel(model);

            return View(model);
        }        
    }
}