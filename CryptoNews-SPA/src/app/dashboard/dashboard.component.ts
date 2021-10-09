import { Component, OnInit } from '@angular/core';
import { News } from '../shared/news.model';
import { NewsService } from '../shared/news.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  newsCollection: News[] =[];

  constructor(private newsSvc: NewsService) { }

  ngOnInit(): void {
    this.getNews();
  }

  getNews(): void {
    this.newsSvc.getTopNews()
    .subscribe(news => this.newsCollection = news);
  }
}
