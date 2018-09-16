namespace Lab1 {
    export class ChartModel {
        public x: number;
        public y: number;
    }

    export class Main {
        
        ElementIDs = {
            PolygonChart: "polygonChart",
            Sample: "inputData",
            UseVirtualFrequency: "IsVirtual",
        };

        ElementClasses = {
            RowByFrequency: "row-by-frequency",
            RowByVirtualFrequency: "row-by-virtual-frequency",
        };

        data: ChartModel[];

        constructor() {
            this.initialize();            
        }

        initialize() {
            this.initializeData();
            this.initializeUIControllers();
            this.initializeCharts();
        }

        initializeData() {
            let self = this;
            $.ajax({
                url: "Home/LoadData",
                async: false,
                method: "POST",
                success: (response) => {
                    self.data = response.Data;

                    let dataString = "";
                    response.Sample.forEach((elem) => {
                        dataString += elem + ", ";
                    });
                    $('#' + self.ElementIDs.Sample).text(dataString);
                }
            });
        }

        initializeCharts() {
            let self = this;
            let polygonCanvas = <HTMLCanvasElement>document.getElementById(self.ElementIDs.PolygonChart);
            let polygonContext = polygonCanvas.getContext("2d");
            let polygonChart = self.initializePolygonChart(polygonContext, "Полігон частот", self.data);
            
        }

        initializeUIControllers() {
            let self = this;
            let startVisability = $('input[name=' + self.ElementIDs.UseVirtualFrequency + ']:checked').val() === "True";
            if (startVisability) {
                $('.' + self.ElementClasses.RowByFrequency).each((i, elem) => {
                    $(elem).addClass('display-hide');
                });
            }
            else {
                $('.' + self.ElementClasses.RowByVirtualFrequency).each((i, elem) => {
                    $(elem).addClass('display-hide');
                });
            }

            $('input[name=' + self.ElementIDs.UseVirtualFrequency + ']').change(function () {
                let isVirtual = $('input[name=' + self.ElementIDs.UseVirtualFrequency + ']:checked').val() === "True";
                if (isVirtual) {
                    $('.' + self.ElementClasses.RowByFrequency).each((i, elem) => {
                        $(elem).addClass('display-hide');
                    });

                    $('.' + self.ElementClasses.RowByVirtualFrequency).each((i, elem) => {
                        $(elem).removeClass('display-hide');
                    });
                }
                else {
                    $('.' + self.ElementClasses.RowByFrequency).each((i, elem) => {
                        $(elem).removeClass('display-hide');
                    });
                    $('.' + self.ElementClasses.RowByVirtualFrequency).each((i, elem) => {
                        $(elem).addClass('display-hide');
                    });
                }
            });
        }

        initializePolygonChart(context: CanvasRenderingContext2D, label: string, data: ChartModel[]): Chart {
            let chart = new Chart(context, {
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
        }

    }

    let main = new Main();
}