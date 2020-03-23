import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HackerNewsStory } from '../models/hackernewsstory';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  private _baseUrl: string;

  constructor(private _http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  fetchLatestHackerNewsStories(): Observable<HackerNewsStory[]> {
    return this._http.get<HackerNewsStory[]>(`${this._baseUrl}api/news/latesthackernewsstories`);
  }
}
