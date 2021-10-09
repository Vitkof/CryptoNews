import { Component, OnInit } from '@angular/core';
import { News } from '../shared/news.model';
import { NewsService } from '../shared/news.service';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {
  newsCollection: News[] = [];
  selectNews?: News;

  constructor(private newsSvc: NewsService) { }

  ngOnInit(): void {
    this.getNews();
  }

  getNews(): void {
    this.newsCollection = this.newsSvc.getAllNews();
  }

  select(news: News) {
    console.log(news);
    this.selectNews = news;
  }
}
