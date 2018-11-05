namespace Lab4 {
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
            InputTable: "inputData",
        };        

        constructor() {
            this.initialize();            
        }

        initialize() {

        }        
    }

    let main = new Main();
}