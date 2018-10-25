var Lab2;
(function (Lab2) {
    var TableModel = (function () {
        function TableModel(x, y) {
            this.x = x;
            this.y = y;
        }
        return TableModel;
    }());
    Lab2.TableModel = TableModel;
    var ChartModel = (function () {
        function ChartModel(x, y) {
            this.x = x;
            this.y = y;
        }
        return ChartModel;
    }());
    Lab2.ChartModel = ChartModel;
    var DatasetModel = (function () {
        function DatasetModel(data, showLine, showPoint) {
            this.showLine = showLine;
            this.fill = false;
            this.tension = 0;
            this.data = data;
            this.backgroundColor = ['rgba(255, 99, 132, 0.2)'];
            this.borderColor = ['rgba(255,99,132,1)'];
            this.borderWidth = 2;
            this.pointRadius = showPoint ? 5 : 0;
        }
        return DatasetModel;
    }());
    Lab2.DatasetModel = DatasetModel;
    var Main = (function () {
        function Main() {
            this.ElementIDs = {
                UseVirtualFrequency: "IsVirtual",
                SampleTable: "inputData",
                HistogramChart: "histogramChart",
                VirtualHistogramChart: "virtualHistogramChart",
                CumulusChart: "cumulusChart",
                VirtualCumulusChart: "virtualCumulusChart",
                EmpericalChart: "empericalChart",
                DiscreteRowByFrequencyTable: "discreteRowByFrequency",
                IntervalRowByFrequencyTable: "intervalRowByFrequency",
                IntervalRowByVirtualFrequencyTable: "intervalRowByVirtualFrequency",
            };
            this.ElementClasses = {
                RowByFrequency: "row-by-frequency",
                RowByVirtualFrequency: "row-by-virtual-frequency",
            };
            this.initialize();
        }
        Main.prototype.initialize = function () {
            this.initializeUIControllers();
            this.initializeCharts();
        };
        Main.prototype.initializeCharts = function () {
            var self = this;
            var histogramCanvas = document.getElementById(self.ElementIDs.HistogramChart);
            var virtualHistogramCanvas = document.getElementById(self.ElementIDs.VirtualHistogramChart);
            var cumulusCanvas = document.getElementById(self.ElementIDs.CumulusChart);
            var virtualCumulusCanvas = document.getElementById(self.ElementIDs.VirtualCumulusChart);
            var empericalFunctionCanvas = document.getElementById(self.ElementIDs.EmpericalChart);
            var histogramContext = histogramCanvas.getContext("2d");
            var virtualHistogramContext = virtualHistogramCanvas.getContext("2d");
            var cumulusContext = cumulusCanvas.getContext("2d");
            var virtualCumulusContext = virtualCumulusCanvas.getContext("2d");
            var empericalFunctionContext = empericalFunctionCanvas.getContext("2d");
            $.ajax({
                url: "Home/LoadData",
                async: false,
                method: "POST",
                success: function (response) {
                    self.initializeTable(response.DiscreteRowByFrequency, $('#' + self.ElementIDs.DiscreteRowByFrequencyTable), "zᵢ", true);
                    self.initializeTable(response.IntervalRowByFrequency, $('#' + self.ElementIDs.IntervalRowByFrequencyTable), "mᵢ", true);
                    self.initializeTable(response.IntervalRowByVirtualFrequency, $('#' + self.ElementIDs.IntervalRowByVirtualFrequencyTable), "p*ᵢ", true);
                    var histogramChart = self.initializeBarChart(histogramContext, "Гістограма частот", response.HistogramByFrequency, '(Xᵢ*; Xᵢ₊₁*)', 'mᵢ/r');
                    var virtualHistogramChart = self.initializeBarChart(virtualHistogramContext, "Гістограма відносних частот", response.HistogramByVirtualFrequency, '(Xᵢ*; Xᵢ₊₁*)', 'p*ᵢ/r');
                    var cumulusChart = self.initializeScatterChart(cumulusContext, "Кумулята частот", response.CumulusFrequency, 'Xᵢ', '∑mᵢ');
                    var virtualCumulusChart = self.initializeScatterChart(virtualCumulusContext, "Кумулята відносних частот", response.CumulusVirtualFrequency, 'Xᵢ', '∑p*ᵢ');
                    var empericalFunctionChart = self.initializeEmpericalChart(empericalFunctionContext, response.EmpericalFunction, 'Xᵢ', 'F*(Xᵢ)');
                }
            });
        };
        Main.prototype.initializeUIControllers = function () {
            var self = this;
            var startVisability = $('input[name=' + self.ElementIDs.UseVirtualFrequency + ']:checked').val() === "True";
            if (startVisability) {
                $('.' + self.ElementClasses.RowByFrequency).each(function (i, elem) {
                    $(elem).addClass('display-hide');
                });
            }
            else {
                $('.' + self.ElementClasses.RowByVirtualFrequency).each(function (i, elem) {
                    $(elem).addClass('display-hide');
                });
            }
            $('input[name=' + self.ElementIDs.UseVirtualFrequency + ']').change(function () {
                var isVirtual = $('input[name=' + self.ElementIDs.UseVirtualFrequency + ']:checked').val() === "True";
                if (isVirtual) {
                    $('.' + self.ElementClasses.RowByFrequency).each(function (i, elem) {
                        $(elem).addClass('display-hide');
                    });
                    $('.' + self.ElementClasses.RowByVirtualFrequency).each(function (i, elem) {
                        $(elem).removeClass('display-hide');
                    });
                }
                else {
                    $('.' + self.ElementClasses.RowByFrequency).each(function (i, elem) {
                        $(elem).removeClass('display-hide');
                    });
                    $('.' + self.ElementClasses.RowByVirtualFrequency).each(function (i, elem) {
                        $(elem).addClass('display-hide');
                    });
                }
            });
        };
        Main.prototype.initializeScatterChart = function (context, label, data, axesXLable, axesYLabel) {
            var chart = new Chart(context, {
                type: 'scatter',
                data: {
                    datasets: [{
                            label: label,
                            showLine: true,
                            fill: false,
                            tension: 0,
                            data: data,
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.2)'
                            ],
                            borderColor: [
                                'rgba(255,99,132,1)'
                            ],
                            borderWidth: 1
                        }]
                },
                options: {
                    scales: {
                        xAxes: [{
                                position: 'bottom',
                                type: 'linear',
                                gridLines: {
                                    display: false
                                },
                                ticks: {
                                    max: data[data.length - 1].x + 2,
                                    min: data[0].x - 2
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: axesXLable,
                                    fontSize: 16,
                                    fontStyle: 'bold'
                                }
                            }],
                        yAxes: [{
                                ticks: {
                                    min: 0
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: axesYLabel,
                                    fontSize: 16,
                                    fontStyle: 'bold'
                                }
                            }]
                    },
                }
            });
            return chart;
        };
        Main.prototype.initializeBarChart = function (context, label, data, axesXLable, axesYLabel) {
            var labels = [];
            var columns = [];
            for (var i = 0; i < data.length; i++) {
                labels.push(data[i].x);
                columns.push(data[i].y);
            }
            var chart = new Chart(context, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                            label: label,
                            data: columns,
                            backgroundColor: 'rgba(255, 99, 132, 0.2)',
                            borderColor: 'rgba(255,99,132,1)',
                            borderWidth: 2,
                        }]
                },
                options: {
                    scales: {
                        xAxes: [{
                                position: 'bottom',
                                barPercentage: 1,
                                categoryPercentage: 1,
                                scaleLabel: {
                                    display: true,
                                    labelString: axesXLable,
                                    fontSize: 16,
                                    fontStyle: 'bold'
                                }
                            }],
                        yAxes: [{
                                scaleLabel: {
                                    display: true,
                                    labelString: axesYLabel,
                                    fontSize: 16,
                                    fontStyle: 'bold'
                                }
                            }]
                    },
                }
            });
            return chart;
        };
        Main.prototype.initializeTable = function (data, tableSelector, tableType, useFirstColumn) {
            var tableHeader = "<tr>";
            var tableRow = "<tr>";
            if (useFirstColumn) {
                tableHeader += "<th>(Xᵢ*; Xᵢ₊₁*)</th>";
                tableRow += "<td>" + tableType + "</td>";
            }
            for (var i = 0; i < data.length; i++) {
                tableHeader += "<th>" + data[i].x + "</th>";
                tableRow += "<td>" + data[i].y + "</td>";
            }
            tableHeader += "</tr>";
            tableRow += "</tr>";
            $(tableSelector).append(tableHeader);
            $(tableSelector).append(tableRow);
        };
        Main.prototype.initializeEmpericalChart = function (context, data, axesXLable, axesYLabel) {
            var datasets = [];
            var points = [];
            datasets.push(new DatasetModel([new ChartModel(-100, data[0].y), data[0]], true, false));
            for (var i = 1; i < data.length; i++) {
                var leftPoint = new ChartModel(data[i - 1].x, data[i].y);
                datasets.push(new DatasetModel([leftPoint, data[i]], true, false));
                points.push(leftPoint);
            }
            datasets.push(new DatasetModel([data[data.length - 1], new ChartModel(100, data[data.length - 1].y)], true, false));
            datasets.push(new DatasetModel(points, false, true));
            var chart = new Chart(context, {
                type: 'scatter',
                data: {
                    datasets: datasets,
                },
                options: {
                    legend: {
                        display: false
                    },
                    scales: {
                        xAxes: [{
                                position: 'bottom',
                                type: 'linear',
                                gridLines: {
                                    display: false
                                },
                                ticks: {
                                    max: data[data.length - 1].x + 2,
                                    min: data[0].x - 2
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: axesXLable,
                                    fontSize: 16,
                                    fontStyle: 'bold'
                                }
                            }],
                        yAxes: [{
                                ticks: {
                                    min: 0,
                                    max: 1
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: axesYLabel,
                                    fontSize: 16,
                                    fontStyle: 'bold'
                                }
                            }]
                    },
                }
            });
            return chart;
        };
        return Main;
    }());
    Lab2.Main = Main;
    var main = new Main();
})(Lab2 || (Lab2 = {}));
//# sourceMappingURL=main.js.map