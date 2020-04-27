import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor() { }

  showMessage(type: MessageType, message:string, duration?: number ){
    ($('body') as any)
    .toast({
      class: type,
      showIcon: false,
      showDuration: duration ? duration : 4000,
      message: message
    });
  }



}

export enum MessageType{
  INFO = 'info',
  SUCCESS = 'success',
  WARNING = 'warning',
  ERROR = 'error',
  
}
