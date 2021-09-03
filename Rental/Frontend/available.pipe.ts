import { Pipe, PipeTransform } from "@angular/core";
import { Bike } from "./bikes.page";

@Pipe({
    name: "available"
})
export class AvailablePipe implements PipeTransform {
    transform(bike: Bike) { return  AvailablePipe.transform(bike); }

    static transform(value: Bike): boolean {
        return value?.status?.Case == "Bookable";
    }
}
