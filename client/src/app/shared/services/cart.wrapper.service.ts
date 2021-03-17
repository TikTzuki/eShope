import { SecurityService } from './security.service';
import { ISku } from '../models/sku.model';
import { IOrder } from '../models/order.model';
import { IOrderItem } from '../models/orderItem.model';
import { Guid } from '../../../guid';
import { Injectable } from "@angular/core";
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartWrapperService {
  public cart: IOrder;
  
  constructor(private identityService: SecurityService) {
  }
  private addItemToBasketSource = new Subject<IOrderItem>();
  addItemToBasket$ = this.addItemToBasketSource.asObservable();

  private orderCreatedSouce = new Subject();
  orderCreated$ = this.orderCreatedSouce.asObservable();

  addItemToCart(item: IOrderItem){
    if(this.identityService.IsAuthorized){
      this.addItemToBasketSource.next(item);
    } else {
      this.identityService.Authorize();
    }
  }

  orderCreate(){
    this.orderCreatedSouce.next();
  }
}
