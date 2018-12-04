var Lab7;
(function (Lab7) {
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
    Lab7.DatasetModel = DatasetModel;
    var Main = (function () {
        function Main() {
            this.ElementIDs = {
                SqrMethodChartId: "sqrMethodChart",
                CoefMethodChartId: "coefMethodChart",
            };
            this.initialize();
        }
        Main.prototype.initialize = function () {
            var self = this;
            var sqrMethodCanvas = document.getElementById(self.ElementIDs.SqrMethodChartId);
            var coefMethodCanvas = document.getElementById(self.ElementIDs.CoefMethodChartId);
            var sqrMethodContext = sqrMethodCanvas.getContext("2d");
            var coefMethodContext = coefMethodCanvas.getContext("2d");
            $.ajax({
                url: "Home/LoadData",
                async: false,
                method: "POST",
                success: function (response) {
                    var sqrMethodChart = self.initializeChart(sqrMethodContext, response.SqrMethodChartData, "X", "Y");
                    var coefMethodChart = self.initializeChart(coefMethodContext, response.CoefMethodChartData, "X", "Y");
                }
            });
        };
        Main.prototype.initializeChart = function (context, data, axesXLable, axesYLabel) {
            var datasets = [];
            datasets.push(new DatasetModel(data[0], true, false));
            datasets.push(new DatasetModel(data[1], true, false));
            datasets.push(new DatasetModel(data[2], false, true));
            var points = data[2];
            var minX = points[0].x, minY = points[0].y, maxX = points[0].x, maxY = points[0].y;
            for (var i = 1; i < points.length; i++) {
                if (points[i].x < minX) {
                    minX = points[i].x;
                }
                if (points[i].x > maxX) {
                    maxX = points[i].x;
                }
                if (points[i].y < minY) {
                    minY = points[i].y;
                }
                if (points[i].y > maxY) {
                    maxY = points[i].y;
                }
            }
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
                                    min: minX - 1,
                                    max: maxX + 1
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
                                    min: minY - 1,
                                    max: maxY + 1
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
    Lab7.Main = Main;
    var main = new Main();
})(Lab7 || (Lab7 = {}));
//# sourceMappingURL=main.js.map