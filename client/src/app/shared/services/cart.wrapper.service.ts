import { SecurityService } from './security.service';
import { ISku } from '../models/sku.model';
import { IOrder } from '../models/order.model';
import { IOrderItem } from '../models/orderItem.model';
import { Guid } from '../../../guid';
import { Injectable } from "@angular/core";
import { Subject } from 'rxjs';
import { ICartItem } from '../models/cartItem.model';
import { ICart } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class CartWrapperService {
  public cart: ICart;
  
  constructor(private identityService: SecurityService) {
  }
  private addItemToCartSource = new Subject<ICartItem>();
  addItemToCart$ = this.addItemToCartSource.asObservable();

  private orderCreatedSouce = new Subject();
  orderCreated$ = this.orderCreatedSouce.asObservable();

  addItemToCart(item: ICartItem){
    // if(this.identityService.IsAuthorized){
      console.log(item);
      
      this.addItemToCartSource.next(item);
    // } else {
      // this.identityService.Authorize();
    // }
  }

  orderCreate(){
    this.orderCreatedSouce.next();
  }
}
