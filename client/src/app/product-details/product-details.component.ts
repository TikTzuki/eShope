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
import numberOnly from '../shared/util/validate';
import { ICartItem } from '../shared/models/cartItem.model';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  authSubscription: Subscription;
  authenticated: boolean;
  product: IProduct;
  selectedSku: ISku;
  selectedImageUrl: string;
  quantity: number = 0;
  skuForm: FormGroup;
    slideConfig = {
    "slidesToShow": 2,
    "slidesToScroll": 1,
    "nextArrow": "<div class='nav-btn next-slide'></div>",
    "prevArrow": "<div class='nav-btn prev-slide'></div>",
    // "dots": true,
    // "infinite": false
  };

  constructor(
    private route: ActivatedRoute,
    private service: ProductDetailService,
    private configurationService: ConfigurationService,
    private cartEventService: CartWrapperService,
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
    this.skuForm = this.formBuilder.group({
      byParams: new FormControl(`{"skuId":"${this.selectedSku.id
      }", "name":"${this.product.productName
      }", "variation":"${this.selectedSku.color +' '+ this.selectedSku.size
      }", "itemPrice":"${this.selectedSku.price
      }", "quantity":"${this.quantity
      }", "image":"${this.selectedSku.images[0].url}" }`)
    });
  }

  loadProduct(productId: number): void {
    this.service.getProduct(productId)
      .subscribe(product => {
        this.product = product;
        this.selectedSku = product.skus[0];
        this.selectedImageUrl = this.selectedSku.images[0].url;
        this.loadFromSku();
      });
  }

  addToCart(event: any) {
    const cartItem: ICartItem = JSON.parse(this.skuForm.value.byParams);
    this.cartEventService.addItemToCart(cartItem);

  }

  onSkuChanged(event: any, value: ISku) {
    this.selectedSku = value;
  }

  onImageSelected(event:any, value: string){
    this.selectedImageUrl = value;
  }

  onQuantityChanged(event: any) {
    if(event.target.value > this.selectedSku.available){
      event.target.value = this.selectedSku.available;
      this.quantity = this.selectedSku.available;
    }
    this.loadFromSku();
  }

  numberOnly(event: any){
    return numberOnly(event);
  }
}
