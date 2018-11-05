var Lab3;
(function (Lab3) {
    var ChartModel = (function () {
        function ChartModel(x, y) {
            this.x = x;
            this.y = y;
        }
        return ChartModel;
    }());
    Lab3.ChartModel = ChartModel;
    var Main = (function () {
        function Main() {
            this.ElementIDs = {
                SampleTable: "inputData",
            };
            this.initialize();
        }
        Main.prototype.initialize = function () {
            this.initializeCharts();
        };
        Main.prototype.initializeCharts = function () {
            var self = this;
            $.ajax({
                url: "Home/LoadData",
                async: false,
                method: "POST",
                success: function (response) {
                    self.initializeTable(response.Sample, $('#' + self.ElementIDs.SampleTable), "Xᵢ", false);
                }
            });
        };
        Main.prototype.initializeTable = function (data, tableSelector, tableType, useFirstColumn) {
            var tableRow = "<tr>";
            if (useFirstColumn)
                tableRow += "<th>Xᵢ</th>";
            for (var i = 1; i <= data.length; i++) {
                tableRow += "<th>X" + i + "</th>";
            }
            tableRow += "</tr>";
            $(tableSelector).append(tableRow);
            tableRow = "<tr>";
            if (useFirstColumn)
                tableRow += "<td>" + tableType + "</td>";
            for (var i = 0; i < data.length; i++) {
                tableRow += "<td>" + data[i].y + "</td>";
            }
            tableRow += "</tr>";
            $(tableSelector).append(tableRow);
        };
        return Main;
    }());
    Lab3.Main = Main;
    var main = new Main();
})(Lab3 || (Lab3 = {}));
//# sourceMappingURL=main.js.map