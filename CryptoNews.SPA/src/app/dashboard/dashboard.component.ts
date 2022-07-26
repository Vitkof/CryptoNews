import { News } from './../../models/news';
import { Component, OnInit } from '@angular/core';
import { NewsService } from '../services/news.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  newsCollection: News[] = [];

  constructor(private newsService: NewsService) {
  }

 ngOnInit(): void {
   this.getNews();
 }

 getNews(): void {
  this.newsService.getTopRatedNews()
    .subscribe(news => this.newsCollection = news);
 }
}
