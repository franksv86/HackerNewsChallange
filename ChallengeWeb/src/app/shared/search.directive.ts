import { Directive, ElementRef, Output, EventEmitter } from '@angular/core';
import { fromEvent } from 'rxjs';
import { pluck, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Directive({
  selector: '[inputsearch]'
})
export class SearchDirective {

  @Output()
  inputsearch: EventEmitter<any> = new EventEmitter();

  constructor(private el: ElementRef) {
    fromEvent(this.el.nativeElement, 'input')
      .pipe(
        pluck("target"),
        pluck("value"),
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe(x => {
        this.inputsearch.emit(x);
      });
  }


}
