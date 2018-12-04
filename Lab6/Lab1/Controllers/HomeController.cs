using Lab6.Services;
using Lab6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab6.Controllers
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
                Z = ExperimentResultsService.Z,
                Kxy = ExperimentResultsService.Kxy,
                S0x = ExperimentResultsService.S0x,
                S0y = ExperimentResultsService.S0y,
                Rxy = ExperimentResultsService.rxy,
                R1xy001 = ExperimentResultsService.GetR1xy(0.01),
                R2xy001 = ExperimentResultsService.GetR2xy(0.01),
                R1xy005 = ExperimentResultsService.GetR1xy(0.05),
                R2xy005 = ExperimentResultsService.GetR2xy(0.05),
                BigKxy = ExperimentResultsService.BigKxy,
                BigS0x = ExperimentResultsService.BigS0x,
                BigS0y = ExperimentResultsService.BigS0y,
                BigRxy = ExperimentResultsService.BigRxy,
                RomCoef = ExperimentResultsService.RomCoef
            };
            ExperimentResultsService.RoundOutputViewModel(model);

            return View(model);
        }
    }
}