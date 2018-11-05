var Lab4;
(function (Lab4) {
    var ChartModel = (function () {
        function ChartModel(x, y) {
            this.x = x;
            this.y = y;
        }
        return ChartModel;
    }());
    Lab4.ChartModel = ChartModel;
    var Main = (function () {
        function Main() {
            this.ElementIDs = {
                InputTable: "inputData",
            };
            this.initialize();
        }
        Main.prototype.initialize = function () {
        };
        return Main;
    }());
    Lab4.Main = Main;
    var main = new Main();
})(Lab4 || (Lab4 = {}));
//# sourceMappingURL=main.js.map