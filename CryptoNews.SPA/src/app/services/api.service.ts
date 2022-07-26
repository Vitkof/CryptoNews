import { environment } from './../../environments/environment';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as queryString from 'query-string';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) {
  }

  get(urlPath: string, data: object = {}): Observable<any> {
    if (Object.keys(data).length > 0) {
      urlPath += '?' + queryString.stringify(data);
    }
    return this.http.get(this.url(urlPath));
  }

  post(urlPath: string, data: object, options: object = {}): Observable<any> {
    return this.http.post(this.url(urlPath), data, options);
  }

  patch(urlPath: string, data: object, options: object = {}): Observable<any> {
    return this.http.patch(this.url(urlPath), data, options);
  }

  delete(urlPath: string): Observable<any> {
    return this.http.delete(this.url(urlPath));
  }

  put(urlPath: string, data: object): Observable<any> {
    return this.http.put(this.url(urlPath), data);
  }

  request(method: string, urlPath: string, data: object, options: object): Observable<any> {
    const req = new HttpRequest(method, this.url(urlPath), data, options);
    return this.http.request(req);
  }

  private url(path: string) {
    return environment.apiUrl + path;
  }
}
