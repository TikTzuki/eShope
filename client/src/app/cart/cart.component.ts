import { Subscription, Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CartWrapperService } from '../shared/services/cart.wrapper.service';
import { IOrder } from '../shared/models/order.model';
import { CartService } from './cart.service';
import { IOrderItem } from '../shared/models/orderItem.model';
import { error } from 'protractor';
import { ICart } from '../shared/models/cart.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  errorMessages: any;
  cart: ICart;
  totalPrice: number = 0;

  constructor(
    private service: CartService,
    private router: Router,
    private cartWrapper: CartWrapperService) { }

  ngOnInit(): void {
    this.service.getCart().subscribe({
      next: (cart: any) => {
          this.cart = cart;
          this.calculateTotalPrice();
      }
    });
  }

  itemQuantityChanged(item: IOrderItem): void{
    this.calculateTotalPrice();
    this.service.setCart(this.cart).subscribe({
      next: x => console.log('cart updated:' + x)
    });
  }

  update(event: any): Observable<boolean> {
    const setCartObservable = this.service.setCart(this.cart);
    // tslint:disable-next-line: deprecation
    setCartObservable
      .subscribe(
        x => {
          this.errorMessages = [];
          console.log('cart updated:' + x);
        },
        errorMessage => this.errorMessages = errorMessage.messages);
    return setCartObservable;
  }

  checkOut(event: any){
    this.update(event)
    .subscribe(
      x=>{
        this.errorMessages = [];
        this.cartWrapper.cart = this.cart;
        this.router.navigate(['order']);
      });
  }

  private calculateTotalPrice(){
    this.totalPrice = 0;
    this.cart.items.forEach(item => {
      this.totalPrice += (item.itemPrice * item.quantity);
    })
  }
}
