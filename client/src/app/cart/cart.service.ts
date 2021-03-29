import { Injectable } from "@angular/core";
import { Subject, Observable, observable } from 'rxjs';
import { DataService } from '../shared/services/data.service';
import { SecurityService } from '../shared/services/security.service';
import { CartWrapperService } from '../shared/services/cart.wrapper.service';
import { ConfigurationService } from "../shared/services/configuration.service";
import { StorageService } from '../shared/services/storage.service';
import { tap } from "rxjs/operators";
import { ICart } from '../shared/models/cart.model';
import { ICartItem } from '../shared/models/cartItem.model';
import { ISku } from '../shared/models/sku.model';
import { IAddress } from '../shared/models/customer.model';
import { IOrder } from '../shared/models/order.model';
import { Router } from '@angular/router';
import { Address } from "node:cluster";
import { DatePipe } from "@angular/common";
import { DateFormat } from "../shared/util/date.format";
import { IOrderItem } from '../shared/models/orderItem.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartUrl: string = '';
  private purchaseUrl: string = '';
  cart: ICart = {
    id: null,
    customerId: null,
    shippingFee: null,
    totalPrice: null,
    items: []
  };

  private cartDropedSource = new Subject();
  cartDroped$ = this.cartDropedSource.asObservable();

  constructor(
    private service: DataService,
    private authService: SecurityService,
    private cartEvents: CartWrapperService,
    private router: Router,
    private configurationService: ConfigurationService,
    private storageService: StorageService
  ) {
    console.log('security service is authorized ' + authService.IsAuthorized);
    if (this.authService.IsAuthorized) {
      if(this.authService.UserData){
        // this.cart.customerId = this.authService.UserData.sub;
        this.cart.customerId = this.authService.UserData.id;
        if(this.configurationService.isReady){
          this.cartUrl = this.configurationService.serverSettings.purchaseUrl + '/api/customer/cart';
          this.purchaseUrl = this.configurationService.serverSettings.purchaseUrl;
          this.loadData();
        } else {
          this.configurationService.settingLoaded$.subscribe(x => {
            this.cartUrl = this.configurationService.serverSettings.purchaseUrl + '/api/customer/cart';
            this.purchaseUrl = this.configurationService.serverSettings.purchaseUrl;
            this.loadData();
          });
        }
      }
    }
    this.cartEvents.orderCreated$.subscribe(x => {
      this.dropCart();
    });
  }

  addItemToCart(newItem: ICartItem): Observable<boolean> {
    console.log('add Item to cart');
    let isNewItem = true;
    this.cart.items.forEach(item => {
      if(item.skuId === newItem.skuId){
        item.quantity = Number(item.quantity) + Number(newItem.quantity);
        console.log("quantity: " + item.quantity);
        isNewItem = false;
        return;
      }
    });
    if(isNewItem){
      this.cart.items.push(newItem);
    }
    return this.setCart(this.cart);
  }

  removeCartItem(itemRemove: ICartItem): Observable<boolean>{
    this.cart.items.forEach((item, index) => {
      if (item.skuId === itemRemove.skuId) {
        console.log('finded for remove' + index);
        this.cart.items.splice(index, 1);
        console.log(this.cart);
        return;
      }
    })
    return this.setCart(this.cart);
  }

  getSkuMappingItem(item: ICartItem): Observable<ISku> {
    const url = this.purchaseUrl + '/api/skus/' + item.skuId;
    return this.service.get(url).pipe<ISku>(tap({
      next: (response: any) => {
        if (response.status === 204) {
          return null;
        }
        return response;
      }
    }));
  }

  setCart(cart: ICart): Observable<boolean> {
    console.log('set cart', cart);
    const url = this.cartUrl + '/'+ cart.customerId;
    return this.service.put(url, cart).pipe<boolean>(tap((response: any) => response));
  }

  createOrder(cartCheckout, paymentMethod, address): Observable<IOrder> {
    const url = this.purchaseUrl + '/api/orders';
    const order = this.mapCartInfoCheckout(cartCheckout, paymentMethod, address);
    return this.service.post(url, order).pipe<IOrder>(tap((res: any) => {
      return res;
    }));
  }

  setCartEmpty(): Observable<ICart>{
    const url = this.cartUrl + '/' + this.cart.customerId;
    let cart: ICart = {
      customerId: this.cart.customerId,
      id: this.cart.id,
      items: [],
      shippingFee: 0,
      totalPrice: 0
    };
    return this.service.put(url, cart).pipe<ICart>(tap((response: any) => {
      this.cartEvents.orderCreate();
      return response;
    }));
  }

  getCart(): Observable<ICart> {
    const url = this.cartUrl + '/' + this.cart.customerId;
    return this.service.get(url).pipe<ICart>(tap(
      {
        next: (response: any) => {
          if (response.status === 204) {
            return null;
          }
          console.log('get cart');
          return response;
        }
      }
    ));
  }

  getAddress(): Observable<IAddress[]>{
    const url = this.purchaseUrl + '/address?customerId=' + this.cart.customerId;
    return this.service.get(url).pipe<IAddress[]>(tap(
      {
        next: (res: any) => {
          if (res.status === 204) {
            return null;
          }
          return res;
        }
      }
    ));
  }

  mapCartInfoCheckout(cart: ICart, paymentMethod, address): IOrder{
    const order: IOrder = {
      customerId: cart.customerId,
      createDate: DateFormat.formatISO(new Date()),
      updateDate: DateFormat.formatISO(new Date()),
      shippingFee: cart.shippingFee,
      shippingAddress: address,
      totalPrice: cart.shippingFee,
      status: 'pending',
      paymentMethod,
      id: null,
      orderItems: cart.items.map((cartItem) => {
        let orderItem: IOrderItem = {
          id: null,
          image: cartItem.image,
          itemPrice: cartItem.itemPrice,
          name: cartItem.name,
          order_id: null,
          quantity: cartItem.quantity,
          sku_id: cartItem.skuId,
          variation: cartItem.variation
        };
        return orderItem;
      })
    };

    return order;
  }

  dropCart() {
    this.cart.items = [];
    this.cartDropedSource.next();
  }

  private loadData() {
    this.getCart().subscribe(cart => {
      if (cart != null) {
        this.cart = cart;
      }
    })
  }
}