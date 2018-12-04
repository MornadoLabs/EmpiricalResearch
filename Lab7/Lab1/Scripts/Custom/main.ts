namespace Lab7 {
    export class DatasetModel {
        constructor(data: ChartPoint[], showLine: boolean, showPoint: boolean) {
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
        public data: ChartPoint[];
        public backgroundColor: string[];
        public borderColor: string[];
        public borderWidth: number;
        public pointRadius: number;
    }

    export class Main {
        
        ElementIDs = {
            SqrMethodChartId: "sqrMethodChart",
            CoefMethodChartId: "coefMethodChart",
        };        

        constructor() {
            this.initialize();            
        }

        initialize() {
            let self = this;

            let sqrMethodCanvas = <HTMLCanvasElement>document.getElementById(self.ElementIDs.SqrMethodChartId);
            let coefMethodCanvas = <HTMLCanvasElement>document.getElementById(self.ElementIDs.CoefMethodChartId);

            let sqrMethodContext = sqrMethodCanvas.getContext("2d");
            let coefMethodContext = coefMethodCanvas.getContext("2d");

            $.ajax({
                url: "Home/LoadData",
                async: false,
                method: "POST",
                success: (response) => {
                    let sqrMethodChart = self.initializeChart(sqrMethodContext, response.SqrMethodChartData, "X", "Y");
                    let coefMethodChart = self.initializeChart(coefMethodContext, response.CoefMethodChartData, "X", "Y");
                }
            });
        }

        initializeChart(context: CanvasRenderingContext2D, data: ChartPoint[][], axesXLable: string, axesYLabel: string): Chart {
            let datasets = [];
            datasets.push(new DatasetModel(data[0], true, false));            
            datasets.push(new DatasetModel(data[1], true, false));
            datasets.push(new DatasetModel(data[2], false, true));

            let points = data[2];
            let minX = points[0].x, minY = points[0].y, maxX = points[0].x, maxY = points[0].y;
            for (let i = 1; i < points.length; i++) {
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
        }
    }

    let main = new Main();
}