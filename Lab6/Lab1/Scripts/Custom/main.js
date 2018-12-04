var Lab6;
(function (Lab6) {
    var ChartModel = (function () {
        function ChartModel(x, y) {
            this.x = x;
            this.y = y;
        }
        return ChartModel;
    }());
    Lab6.ChartModel = ChartModel;
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
    Lab6.Main = Main;
    var main = new Main();
})(Lab6 || (Lab6 = {}));
//# sourceMappingURL=main.js.map