import { Component, Input, OnInit } from '@angular/core';
import { News } from '../shared/news.model';

@Component({
  selector: 'app-news-detail',
  templateUrl: './news-detail.component.html',
  styleUrls: ['./news-detail.component.css']
})
export class NewsDetailComponent implements OnInit {
  @Input() specifiedNews?: News;

  constructor() { }

  ngOnInit(): void {
  }

}
