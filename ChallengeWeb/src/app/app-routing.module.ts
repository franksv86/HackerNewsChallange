import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HackerNewsComponent } from './hacker-news/hacker-news.component';


const routes: Routes = [
  { path: 'news', component: HackerNewsComponent },
  { path: '', redirectTo: 'news', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
