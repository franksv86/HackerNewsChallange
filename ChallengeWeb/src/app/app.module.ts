import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UrlService } from './services/url.service';
import { HttpClientModule } from '@angular/common/http';
import { HackerNewsComponent } from './hacker-news/hacker-news.component';
import { SharedModule } from './shared/shared.module';
import {NgxPaginationModule} from 'ngx-pagination'; 

@NgModule({
  declarations: [
    AppComponent,
    HackerNewsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: configFactory,
      deps: [UrlService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


export function configFactory(config: UrlService) {
  return () => config.loadConfig();
}