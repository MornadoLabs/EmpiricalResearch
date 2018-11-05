namespace Lab3 {
    export class ChartModel {
        constructor(x: number, y: number) {
            this.x = x;
            this.y = y;
        }

        public x: number;
        public y: number;
    }
    
    export class Main {
        
        ElementIDs = {
            SampleTable: "inputData",
        };

        constructor() {
            this.initialize();            
        }

        initialize() {
            this.initializeCharts();
        }        

        initializeCharts() {
            let self = this;

            $.ajax({
                url: "Home/LoadData",
                async: false,
                method: "POST",
                success: (response) => {
                    self.initializeTable(response.Sample, $('#' + self.ElementIDs.SampleTable), "Xᵢ", false);                    
                }
            });            
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
        
    }

    let main = new Main();
}