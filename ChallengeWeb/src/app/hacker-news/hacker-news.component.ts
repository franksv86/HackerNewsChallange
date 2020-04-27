import { Component, OnInit } from '@angular/core';
import { HackerNewsService, HackerNewsModel } from '../services/hacker-news.service';
import { $ } from 'protractor';
import { UtilsService, MessageType } from '../services/utils.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-hacker-news',
  templateUrl: './hacker-news.component.html',
  styleUrls: ['./hacker-news.component.scss']
})
export class HackerNewsComponent implements OnInit {

  list: HackerNewsModel[] = [];
  take: number = 10;
  search: string = "";

  loading: boolean = false;

  subscription: Subscription = null;
  
  constructor(private service: HackerNewsService, private util: UtilsService) {

  }

  ngOnInit(): void {
    this.load();
  }
  
  load() {
    if (this.subscription && !this.subscription.closed)
      this.subscription.unsubscribe();

    this.loading = true;
    this.subscription = this.service.getPage(this.take, this.search).subscribe(x => {
      this.list = x;
      this.loading = false;
    }, () => { this.loading = false; });


  }

  checkLink(item: HackerNewsModel) {
    item.url ? window.location.href = item.url : this.util.showMessage(MessageType.ERROR, "Ups... This story has no link.");
  }

  changeTop(top) {
    this.take = top;
    this.load();

  }


}
