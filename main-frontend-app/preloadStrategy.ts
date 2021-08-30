import { PreloadingStrategy, Route } from "@angular/router";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class PreloadAllModulesStrategy implements PreloadingStrategy {
    preload(route: Route, loadModule: () => Observable<any>): Observable<any> {
        return loadModule();
    }
}
