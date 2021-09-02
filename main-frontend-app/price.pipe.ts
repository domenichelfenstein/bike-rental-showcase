import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "price"
})
export class PricePipe implements PipeTransform {
    transform(value: number | undefined) { return  PricePipe.transform(value); }

    static transform(value: number | undefined): string {
        if (value == undefined) {
            return "???"
        }

        return `${value.toFixed(2)} $`;
    }
}
