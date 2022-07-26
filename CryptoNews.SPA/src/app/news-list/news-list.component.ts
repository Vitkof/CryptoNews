import { Component, OnInit } from '@angular/core';
import { News } from '../../models/news';
import { NewsService } from '../services/news.service';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.scss']
})
export class NewsListComponent implements OnInit {
  newsCollection: News[] = [];
  selectedNews?: News;

  constructor(private newsService: NewsService) { }

  ngOnInit(): void {
    this.getNews();
  }

  getNews() {
    this.newsCollection = this.newsService.getNews();
  }

  select(news: News) {
    console.log(news);
    this.selectedNews = news;
  }
}
