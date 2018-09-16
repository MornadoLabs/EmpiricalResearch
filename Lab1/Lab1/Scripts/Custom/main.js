var Lab1;
(function (Lab1) {
    var ChartModel = (function () {
        function ChartModel() {
        }
        return ChartModel;
    }());
    Lab1.ChartModel = ChartModel;
    var Main = (function () {
        function Main() {
            this.ElementIDs = {
                PolygonChart: "polygonChart",
                Sample: "inputData",
                UseVirtualFrequency: "IsVirtual",
            };
            this.ElementClasses = {
                RowByFrequency: "row-by-frequency",
                RowByVirtualFrequency: "row-by-virtual-frequency",
            };
            this.initialize();
        }
        Main.prototype.initialize = function () {
            this.initializeData();
            this.initializeUIControllers();
            this.initializeCharts();
        };
        Main.prototype.initializeData = function () {
            var self = this;
            $.ajax({
                url: "Home/LoadData",
                async: false,
                method: "POST",
                success: function (response) {
                    self.data = response.Data;
                    var dataString = "";
                    response.Sample.forEach(function (elem) {
                        dataString += elem + ", ";
                    });
                    $('#' + self.ElementIDs.Sample).text(dataString);
                }
            });
        };
        Main.prototype.initializeCharts = function () {
            var self = this;
            var polygonCanvas = document.getElementById(self.ElementIDs.PolygonChart);
            var polygonContext = polygonCanvas.getContext("2d");
            var polygonChart = self.initializePolygonChart(polygonContext, "Полігон частот", self.data);
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
        Main.prototype.initializePolygonChart = function (context, label, data) {
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
                                    labelString: 'Xᵢ',
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
                                    labelString: 'mᵢ',
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
    Lab1.Main = Main;
    var main = new Main();
})(Lab1 || (Lab1 = {}));
//# sourceMappingURL=main.js.map