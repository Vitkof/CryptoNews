import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { AuthService } from './services/auth.service';


class Item {
  purchase: string
  done: boolean
  price: number

  constructor(purchase: string, price: number) {
    this.purchase = purchase
    this.price = price
    this.done = false
  }
}


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  text: string = "";
  price: number = 0;

  title = 'Crypto News';
  currentUser: User;

  constructor(private router: Router,
    private authService: AuthService) {
      this.authService.currentUser.subscribe(u => this.currentUser = u);
    }

  items: Item[] = [
    { purchase: 'Хлеб', done: false, price: 15.9 },
    { purchase: 'Масло', done: false, price: 60 },
    { purchase: 'Картофель', done: true, price: 22.6 },
    { purchase: 'Сыр', done: false, price: 310 },
  ]
  addItem(text: string, price: number): void {
    if (text == null || text.trim() == '' || price == null)
      return
    this.items.push(new Item(text, price))
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
