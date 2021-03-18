import { Injectable } from "@angular/core";
import { Subject, Observable, observable } from 'rxjs';
import { DataService } from '../shared/services/data.service';
import { SecurityService } from '../shared/services/security.service';
import { CartWrapperService } from '../shared/services/cart.wrapper.service';
import { ConfigurationService } from "../shared/services/configuration.service";
import { StorageService } from '../shared/services/storage.service';
import { tap } from "rxjs/operators";
import { ICart } from '../shared/models/cart.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartUrl: string = '';
  private purchaseUrl: string = '';
  cart: ICart = {
    id: null,
    customerId: 1,
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
    // if(this.authService.IsAuthorized){
    //   if(this.authService.UserData){
    //     // TODO:  --------------------------------------
    //     this.cart.customerId = this.authService.UserData.sub;
    //     if(this.configurationService.isReady){
    //       this.cartUrl = this.configurationService.serverSettings.purchaseUrl;
    //       this.purchaseUrl = this.configurationService.serverSettings.purchaseUrl;
    //       this.loadData();
    //     } else {
    this.configurationService.settingLoaded$.subscribe(x => {
      this.cartUrl = this.configurationService.serverSettings.purchaseUrl;
      this.purchaseUrl = this.configurationService.serverSettings.purchaseUrl;
      this.loadData();
    })
    //     }
    //   }
    // }
    this.cartEvents.orderCreated$.subscribe(x => {
      this.dropCart();
    });
  }

  addItemToCart(item): Observable<boolean> {
    console.log('add Item to cart');
    this.cart.items.push(item);
    return this.setCart(this.cart);
  }

  setCart(cart: ICart): Observable<boolean> {
    console.log('set cart');
    let url = this.purchaseUrl + '/api/carts/'+ cart.id;
    this.cart = cart;
    return this.service.put(url, cart).pipe<boolean>(tap((response: any) => true));
  }

  setCartCheckout(cartCheckout): Observable<boolean> {
    const url = this.cartUrl + '/api/orders/checkout';

    return this.service.post(url, cartCheckout).pipe<boolean>(tap((response: any) => {
      this.cartEvents.orderCreate();
      return true;
    }));
  }

  getCart(): Observable<ICart> {
    // TODO: Change orders to cart
    const url = this.cartUrl + '/api/cart/' + this.cart.customerId;
    return this.service.get(url).pipe<ICart>(tap(
      {
        next: (response: any) => {
          if (response.status === 204) {
            return null;
          }
          return response;
        }
      }
    ));
  }

  //mapCartInfoCheckout(order: IOrder): ICartCheckout

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