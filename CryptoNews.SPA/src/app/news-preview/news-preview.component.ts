import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { News } from '../../models/news';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-news-preview',
  templateUrl: './news-preview.component.html',
  styleUrls: ['./news-preview.component.scss']
})
export class NewsPreviewComponent implements OnInit {

  @Input() news?: News;
  constructor(private router: Router,
      private messageService: MessageService) { }

  ngOnInit(): void {
  }

  onClick(news: News) {
    this.messageService.add(`Selected news id=${news.id}`);
  }

  navigate(news: News) {
    this.router.navigate([`sp-news/${news.id}`, {news: news}]);
  }
}
