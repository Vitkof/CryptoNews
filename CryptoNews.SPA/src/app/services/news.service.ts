import { NEWS } from './../../mocks/mock-news';
import { Injectable } from '@angular/core';
import { News } from '../../models/news';
import { Observable, of } from 'rxjs';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor(private apiService: ApiService) { }

  getNews(): News[]{
    return NEWS;
  }

  getTopRatedNews(): Observable<News[]>{
    return this.apiService.get('News').pipe();
  }
}
