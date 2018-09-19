namespace Lab1 {
    export class ChartModel {
        constructor(x: number, y: number) {
            this.x = x;
            this.y = y;
        }

        public x: number;
        public y: number;
    }

    export class DatasetModel {
        constructor(data: ChartModel[], showLine: boolean, showPoint: boolean) {
            this.showLine = showLine;
            this.fill = false;
            this.tension = 0;
            this.data = data;
            this.backgroundColor = ['rgba(255, 99, 132, 0.2)'];
            this.borderColor = ['rgba(255,99,132,1)'];
            this.borderWidth = 2;
            this.pointRadius = showPoint ? 5 : 0;
        }

        public showLine: boolean;
        public fill: boolean;
        public tension: number;
        public data: ChartModel[];
        public backgroundColor: string[];
        public borderColor: string[];
        public borderWidth: number;
        public pointRadius: number;
    }

    export class Main {
        
        ElementIDs = {
            UseVirtualFrequency: "IsVirtual",
            SampleTable: "inputData",

            PolygonChart: "polygonChart",
            VirtualPolygonChart: "virtualPolygonChart",

            CumulusChart: "cumulusChart",
            VirtualCumulusChart: "virtualCumulusChart",

            EmpericalChart: "empericalChart",

            DiscreteRowByFrequencyTable: "discreteRowByFrequency",
            DiscreteRowByVirtualFrequencyTable: "discreteRowByVirtualFrequency",
        };

        ElementClasses = {
            RowByFrequency: "row-by-frequency",
            RowByVirtualFrequency: "row-by-virtual-frequency",
        };

        constructor() {
            this.initialize();            
        }

        initialize() {
            this.initializeUIControllers();
            this.initializeCharts();
        }        

        initializeCharts() {
            let self = this;

            let polygonCanvas = <HTMLCanvasElement>document.getElementById(self.ElementIDs.PolygonChart);
            let virtualPolygonCanvas = <HTMLCanvasElement>document.getElementById(self.ElementIDs.VirtualPolygonChart);
            let cumulusCanvas = <HTMLCanvasElement>document.getElementById(self.ElementIDs.CumulusChart);
            let virtualCumulusCanvas = <HTMLCanvasElement>document.getElementById(self.ElementIDs.VirtualCumulusChart);
            let empericalFunctionCanvas = <HTMLCanvasElement>document.getElementById(self.ElementIDs.EmpericalChart);

            let polygonContext = polygonCanvas.getContext("2d");
            let virtualPolygonContext = virtualPolygonCanvas.getContext("2d");
            let cumulusContext = cumulusCanvas.getContext("2d");
            let virtualCumulusContext = virtualCumulusCanvas.getContext("2d");
            let empericalFunctionContext = empericalFunctionCanvas.getContext("2d");

            $.ajax({
                url: "Home/LoadData",
                async: false,
                method: "POST",
                success: (response) => {
                    self.initializeTable(response.Sample, $('#' + self.ElementIDs.SampleTable), "Xᵢ", false);
                    self.initializeTable(
                        response.DiscreteRowByFrequency,
                        $('#' + self.ElementIDs.DiscreteRowByFrequencyTable),
                        "mᵢ",
                        true
                    );
                    self.initializeTable(
                        response.DiscreteRowByVirtualFrequency,
                        $('#' + self.ElementIDs.DiscreteRowByVirtualFrequencyTable),
                        "p*ᵢ",
                        true
                    );

                    let polygonChart = self.initializeScatterChart(polygonContext, "Полігон частот", response.PolygonFrequency, 'Xᵢ', 'mᵢ');
                    let virtualPolygonChart = self.initializeScatterChart(virtualPolygonContext, "Полігон відносних частот", response.PolygonVirtualFrequency, 'Xᵢ', 'p*ᵢ');
                    let cumulusChart = self.initializeScatterChart(cumulusContext, "Кумулята частот", response.CumulusFrequency, 'Xᵢ', '∑mᵢ');
                    let virtualCumulusChart = self.initializeScatterChart(virtualCumulusContext, "Кумулята відносних частот", response.CumulusVirtualFrequency, 'Xᵢ', '∑p*ᵢ');
                    let empericalFunctionChart = self.initializeEmpericalChart(empericalFunctionContext, response.EmpericalFunction, 'Xᵢ', 'F*(Xᵢ)');
                }
            });            
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

        initializeScatterChart(context: CanvasRenderingContext2D, label: string, data: ChartModel[], axesXLable: string, axesYLabel: string): Chart {
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
        }

        initializeTable(data: ChartModel[], tableSelector: any, tableType: string, useFirstColumn: boolean) {
            var tableRow = "<tr>";
            if (useFirstColumn) tableRow += "<th>Xᵢ</th>";
            for (var i = 1; i <= data.length; i++) {
                tableRow += "<th>X" + i + "</th>";
            }
            tableRow += "</tr>";
            $(tableSelector).append(tableRow);

            tableRow = "<tr>";
            if (useFirstColumn) tableRow += "<td>" + tableType + "</td>";
            for (var i = 0; i < data.length; i++) {
                tableRow += "<td>" + data[i].y + "</td>";
            }
            tableRow += "</tr>";
            $(tableSelector).append(tableRow);
        }

        initializeEmpericalChart(context: CanvasRenderingContext2D, data: ChartModel[], axesXLable: string, axesYLabel: string): Chart {
            let datasets = [];
            let points = [];
            //datasets.push(new DatasetModel(data, false));
            datasets.push(new DatasetModel([new ChartModel(-100, data[0].y), data[0]], true, false));

            for (let i = 1; i < data.length; i++) {
                let leftPoint = new ChartModel(data[i - 1].x, data[i].y);
                datasets.push(new DatasetModel([leftPoint, data[i]], true, false));
                points.push(leftPoint);
            }
            datasets.push(new DatasetModel([data[data.length - 1], new ChartModel(100, data[data.length - 1].y)], true, false));
            datasets.push(new DatasetModel(points, false, true));

            let chart = new Chart(context, {
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
        }
    }

    let main = new Main();
}