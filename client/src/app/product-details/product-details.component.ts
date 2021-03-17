import { IProduct } from '../shared/models/product.model';
import { ConfigurationService } from '../shared/services/configuration.service';
import { Subscription } from 'rxjs';
import { SecurityService } from '../shared/services/security.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDetailService } from './productDetails.service';
import { tap } from 'rxjs/operators';
import { ISku } from '../shared/models/sku.model';
import { CartWrapperService } from '../shared/services/cart.wrapper.service';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  authSubscription: Subscription;
  authenticated: boolean;
  product: IProduct;
  selectedSku: ISku;
  quantity: number;
  skuForm: FormGroup;
  constructor(
    private route: ActivatedRoute,
    private service: ProductDetailService,
    private configurationService: ConfigurationService,
    private cartService: CartWrapperService,
    private formBuilder: FormBuilder,
    private sercurityService: SecurityService) {
      this.authenticated = sercurityService.IsAuthorized;
    }

  ngOnInit(): void {
    if (this.configurationService.isReady) {
      this.loadData();
    } else {
      this.configurationService.settingLoaded$.subscribe(x => {
        this.loadData();
      });
    }

    this.authSubscription = this.sercurityService.authenticationChallenge$.subscribe(res => {
      this.authenticated = res;
    });
  }

  loadData() {
    const routeParams = this.route.snapshot.paramMap;
    const productId = Number(routeParams.get('productId'));

    this.loadProduct(productId);
  }

  loadFromSku(): void {
    const byParams = `{"skuId":"${this.selectedSku.id
      }", "name":"${this.product.productName
      }", "variation":"${this.selectedSku.color + this.selectedSku.size
      }", "itemPrice":"${this.selectedSku.price
      }", "quantity":"${this.quantity
      }" }`;
    this.skuForm = this.formBuilder.group({
      byParams: {byParams}
    });
  }

  loadProduct(productId: number): void {
    this.service.getProduct(productId)
      .subscribe(product => {
        this.product = product;
        this.selectedSku = product.skus[0];
        this.loadFromSku();
      });
  }

  addToCart(event: any) {
    // this.basketService.addItemToBasket(item);
    const orderItem = JSON.parse(this.skuForm.value.byParams);
    this.cartService.addItemToCart(orderItem);

  }

  onSkuChanged(event: any, value: ISku) {
    this.selectedSku = value;
  }

  onQuantityChanged(event: any) {
    if (event.target.value > this.selectedSku.available) {
      event.target.value = this.selectedSku.available;
    }
    if(event.target.value === ""){
      event.target.value = 0;
    }
    this.quantity = event.target.value;
    this.loadFromSku();
  }
}
