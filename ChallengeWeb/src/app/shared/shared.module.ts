import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchDirective } from './search.directive';



@NgModule({
  declarations: [SearchDirective],
  imports: [
    CommonModule
  ],
  exports: [SearchDirective]
})
export class SharedModule { }
