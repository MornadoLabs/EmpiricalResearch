using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab4.Repositories;
using Lab4.Models;

namespace Lab4.Services
{
    public class ExperimentResultsService
    {
        public ExperimentResultsService()
        {
            ExperimentResultsRepository = new ExperimentResultsRepository();
        }

        private ExperimentResultsRepository ExperimentResultsRepository { get; set; }

        public double GetMiddleValueRow(int m)
        {
            double result = 0;
            for (int j = 0; j < ExperimentResultsRepository.N; j++)
            {
                result += ExperimentResultsRepository[m, j];
            }
            return result / ExperimentResultsRepository.N;
        }

        public double GetMiddleValueColumn(int n)
        {
            double result = 0;
            for (int i = 0; i < ExperimentResultsRepository.M; i++)
            {
                result += ExperimentResultsRepository[i, n];
            }
            return result / ExperimentResultsRepository.M;
        }

        public double FullMiddleValue
        {
            get
            {
                double result = 0;
                for (int i = 0; i < ExperimentResultsRepository.M; i++)
                {
                    for (int j = 0; j < ExperimentResultsRepository.N; j++)
                    {
                        result += ExperimentResultsRepository[i, j];
                    }
                }
                return result / (ExperimentResultsRepository.M * ExperimentResultsRepository.N);
            }
        }

        public double Q
        {
            get
            {
                var fullMiddleValue = FullMiddleValue;
                double result = 0;
                for (int i = 0; i < ExperimentResultsRepository.M; i++)
                {
                    for (int j = 0; j < ExperimentResultsRepository.N; j++)
                    {
                        result += Math.Pow(ExperimentResultsRepository[i, j] - fullMiddleValue, 2);
                    }
                }
                return result;
            }
        }

        public double Q1
        {
            get
            {
                var fullMiddleValue = FullMiddleValue;
                double result = 0;
                for (int i = 0; i < ExperimentResultsRepository.M; i++)
                {
                    for (int j = 0; j < ExperimentResultsRepository.N; j++)
                    {
                        var middleValue = GetMiddleValueColumn(j);
                        result += Math.Pow(middleValue - fullMiddleValue, 2);
                    }
                }
                return result;
            }
        }

        public double Q2
        {
            get
            {
                double result = 0;
                for (int i = 0; i < ExperimentResultsRepository.M; i++)
                {
                    for (int j = 0; j < ExperimentResultsRepository.N; j++)
                    {
                        var middleValue = GetMiddleValueColumn(j);
                        result += Math.Pow(ExperimentResultsRepository[i, j] - middleValue, 2);
                    }
                }
                return result;
            }
        }

        public double S0
        {
            get
            {
                return Q / (ExperimentResultsRepository.M * ExperimentResultsRepository.N - 1);
            }
        }

        public double S1
        {
            get
            {
                return Q1 / (ExperimentResultsRepository.M - 1);
            }
        }

        public double S2
        {
            get
            {
                return Q2 / (ExperimentResultsRepository.M * (ExperimentResultsRepository.N - 1));
            }
        }

        public double Fem
        {
            get
            {
                return S1 / S2;
            }
        }

        public double Fcr
        {
            get
            {
                return (ExperimentResultsRepository.M - 1) / (double)(ExperimentResultsRepository.M * (ExperimentResultsRepository.N - 1));
            }
        }

        public List<InputTableViewModel> GetInputTable()
        {
            var result = new List<InputTableViewModel>();

            for (int i = 0; i < ExperimentResultsRepository.M; i++)
            {
                result.Add(new InputTableViewModel
                {
                    Time = i + 1,
                    Experiment1 = ExperimentResultsRepository[i, 0],
                    Experiment2 = ExperimentResultsRepository[i, 1],
                    Experiment3 = ExperimentResultsRepository[i, 2],
                    Experiment4 = ExperimentResultsRepository[i, 3],
                    MiddleValue = GetMiddleValueRow(i)
                });
            }

            result.Add(new InputTableViewModel
            {
                Time = null,
                Experiment1 = GetMiddleValueColumn(0),
                Experiment2 = GetMiddleValueColumn(1),
                Experiment3 = GetMiddleValueColumn(2),
                Experiment4 = GetMiddleValueColumn(3),
                MiddleValue = FullMiddleValue
            });

            return result;
        }

        public void RoundOutputViewModel(OutputViewModel outputViewModel)
        {
            foreach(var row in outputViewModel.InputTable)
            {
                row.Experiment1 = Math.Round(row.Experiment1, 4);
                row.Experiment2 = Math.Round(row.Experiment2, 4);
                row.Experiment3 = Math.Round(row.Experiment3, 4);
                row.Experiment4 = Math.Round(row.Experiment4, 4);
                row.MiddleValue = Math.Round(row.MiddleValue, 4);
            }

            outputViewModel.Q = Math.Round(outputViewModel.Q, 4);
            outputViewModel.Q1 = Math.Round(outputViewModel.Q1, 4);
            outputViewModel.Q2 = Math.Round(outputViewModel.Q2, 4);

            outputViewModel.S0 = Math.Round(outputViewModel.S0, 4);
            outputViewModel.S1 = Math.Round(outputViewModel.S1, 4);
            outputViewModel.S2 = Math.Round(outputViewModel.S2, 4);
            
            outputViewModel.Fem = Math.Round(outputViewModel.Fem, 4);
            outputViewModel.Fcr = Math.Round(outputViewModel.Fcr, 4);
        }
    }
}