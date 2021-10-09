import { HttpClient, HttpRequest } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({ providedIn:'root'})

export class ApiService {
    constructor(private http:HttpClient){}

    private urlBuild(url:string) {
        return environment.apiUrl + url;
    }

    get(url:string, data:object = {}): Observable<any> {
        if(Object.keys(data).length > 0) {
            const querystring = require('querystring');
            url += '?'+querystring.stringify(data);
        }
        return this.http.get(this.urlBuild(url));
    }

    post(url:string, data:object, options:object = {}): Observable<any> {
        return this.http.post(this.urlBuild(url), data, options);
    }

    patch(url:string, data:object, options:object = {}): Observable<any> {
        return this.http.patch(this.urlBuild(url), data, options);
    }

    put(url:string, data:object) {
        return this.http.put(this.urlBuild(url), data);
    }

    delete(url:string) {
        return this.http.delete(this.urlBuild(url));
    }

    request(method:string, url:string, data:object, options:object) {
        const req = new HttpRequest(method, this.urlBuild(url), data, options);
        return this.http.request(req);
    }
}