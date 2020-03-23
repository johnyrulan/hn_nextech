import { Component, OnInit } from '@angular/core';
import { HackerNewsStory } from '../models/hackernewsstory';
import { NewsService } from '../services/news.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public stories: HackerNewsStory[];

  constructor(private newsService: NewsService) {  }

  ngOnInit() {
    this.newsService.fetchLatestHackerNewsStories().subscribe(s => {
      this.stories = s;
    });
  }

}
