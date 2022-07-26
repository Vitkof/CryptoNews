import { Injectable } from '@angular/core';
import { Observable, tap, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  messages: Observable<string[]> = of([]);

  constructor() { }

  getMessages(): Observable<string[]> {
    return this.messages;
  }

  add(message: string) {
    //not work as planned
    this.messages.pipe(tap(messages => {
      messages.push(message);
    }))
  }

  clear() {
    this.messages = of([]);
  }
}
