import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UrlService {

  public static url: string = "";

  constructor(private _http: HttpClient) { }

  loadConfig() {
    return new Promise<boolean>((resolve) => {
      this._http.get('assets/config.json').subscribe((config: any) => {
        UrlService.url = config.apiUrl;
        console.log(UrlService.url)
        resolve(true);
      });
    });
  }
}
