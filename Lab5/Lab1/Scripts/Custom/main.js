var Lab5;
(function (Lab5) {
    var ChartModel = (function () {
        function ChartModel(x, y) {
            this.x = x;
            this.y = y;
        }
        return ChartModel;
    }());
    Lab5.ChartModel = ChartModel;
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
    Lab5.Main = Main;
    var main = new Main();
})(Lab5 || (Lab5 = {}));
//# sourceMappingURL=main.js.map