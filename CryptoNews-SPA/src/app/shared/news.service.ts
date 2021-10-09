import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";

import { ApiService } from "./api.service";
import { NEWS } from "./mock-news";
import { News } from "./news.model";

@Injectable({
    providedIn: 'root'
})

export class NewsService {
    constructor(private apiSvc:ApiService){}

    getAllNews(): News[] {
        return NEWS;
    }

    getTopNews(): Observable<News[]> {
        return this.apiSvc.get('News')
        .pipe();
    }

    getNewsById(id: number): Observable<News> {
        const url = `News/${id}`;
        return this.apiSvc.get(url)
        .pipe();
    }

    searchNews(term: string): Observable<News[]> {
        if (!term.trim()) {
            return of([]);    // empty array
        }
        return this.apiSvc.get(`News/?name=${term}`)
        .pipe();
    }
}