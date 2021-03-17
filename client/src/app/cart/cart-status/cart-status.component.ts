import { Subscription } from 'rxjs';
import { CartService } from '../cart.service';
import { CartWrapperService } from '../../shared/services/cart.wrapper.service';
import { SecurityService } from '../../shared/services/security.service';
import { ConfigurationService } from '../../shared/services/configuration.service';
import { Component, OnInit } from '@angular/core';
import { ignoreElements } from 'rxjs/operators';

@Component({
  selector: 'app-cart-status',
  templateUrl: './cart-status.component.html',
  styleUrls: ['./cart-status.component.css']
})
export class CartStatusComponent implements OnInit {
  cartItemAddedSubscription: Subscription;
  authSubscription: Subscription;
  cartDroppedSubscription: Subscription;

  badge: number = 0;

  constructor(
    private service: CartService,
    private cartEvents: CartWrapperService,
    private authService: SecurityService,
    private configurationService: ConfigurationService
  ) { }

  ngOnInit() {
    // Subcibe to Add Basket Observable:
    this.cartItemAddedSubscription = this.cartEvents.addItemToBasket$.subscribe(
      item => {
        this.service.addItemToCart(item).subscribe(res => {
          this.service.getCart().subscribe(cart => {
            if (cart) {
              this.badge = cart.items.length;
            }
          });
        });
      });

      // Subcribe to Drop Basket Observable:
    this.cartDroppedSubscription = this.service.cartDroped$.subscribe(res => {
      this.badge = 0;
    });

    // Subcribe to login & logout observable
    this.authSubscription = this.authService.authenticationChallenge$.subscribe(res => {
      this.service.getCart().subscribe(cart => {
        if (cart != null) {
          this.badge = cart.items.length;
        }
      });
    });
    // Init:
    if(this.configurationService.isReady){
      this.service.getCart().subscribe(cart =>{
        if(cart != null){
          this.badge = cart.items.length;
        }
      });
    } else {
      this.configurationService.settingLoaded$.subscribe( x =>{
        // TODO: change carts to cart
        this.service.getCart().subscribe(cart => {
          if(cart != null){
            this.badge = cart.items.length;
          }
        });
      });
    }
  }

}

