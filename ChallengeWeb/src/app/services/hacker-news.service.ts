import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UrlService } from './url.service';


@Injectable({
  providedIn: 'root'
})
export class HackerNewsService {

  constructor(private _http: HttpClient) {

  }

  getPage(take: number, search: string): Observable<HackerNewsModel[]> {
    return this._http.get<HackerNewsModel[]>(`${UrlService.url}/HackerNews/getpage?take=${take}&search=${search}`);
  }

}

export class HackerNewsModel {
  by?: string;
  descendants?: number;
  id?: number;
  score?: number;
  time?: number;
  title?: string;
  type?: string;
  url?: string;
  date?: Date;
}


