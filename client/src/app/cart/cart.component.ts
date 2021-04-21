import { Subscription, Observable, concat, interval } from 'rxjs';
import { Component, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CartWrapperService } from '../shared/services/cart.wrapper.service';
import { IOrder } from '../shared/models/order.model';
import { CartService } from './cart.service';
import { IOrderItem } from '../shared/models/orderItem.model';
import { error } from 'protractor';
import { ICart } from '../shared/models/cart.model';
import { ConfigurationService } from '../shared/services/configuration.service';
import { ICartItem } from '../shared/models/cartItem.model';
import { ISku } from '../shared/models/sku.model';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { IAddress } from '../shared/models/customer.model';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faCreditCard, fas } from '@fortawesome/free-solid-svg-icons';
import { faMoneyBillAlt } from '@fortawesome/free-solid-svg-icons';
import { Input } from '@angular/core';
import { ModalDismissReasons, NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { resolve } from 'node:path';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { Modal } from 'bootstrap';
import { SecurityService } from '../shared/services/security.service';
import { EPaymentMethod } from '../shared/models/paymentMethod.const';
@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  errorMessages: any;
  cart: ICart;
  provisional: number = 0;
  shippingFee: number = 0;
  totalPrice: number = 0;
  shippingAddress: IAddress;
  addressJsonString: string = '';
  addressList: IAddress[];
  paymentList: any[] = [
    { name: 'Cash on delivery', icon: 'fas fa-money-bill-alt', value: EPaymentMethod.COD },
    { name: 'Visa', icon: 'fab fa-cc-visa', value: EPaymentMethod.VISA },
    { name: 'Credit card', icon: 'fas fa-credit-card', value: EPaymentMethod.CREDIT },
    { name: 'Bitcoin', icon: 'fab fa-bitcoin', value: EPaymentMethod.BITCOIN },
    { name: 'Paypal', icon: 'fab fa-cc-paypal', value: EPaymentMethod.PAYPAL },
    { name: 'Mastercard', icon: 'fab fa-cc-mastercard', value: EPaymentMethod.MASTERCARD }
  ];
  selectedPayment = this.paymentList[0];
  closeResult = '';

  constructor(
    private service: CartService,
    private router: Router,
    private configurationService: ConfigurationService,
    private cartWrapper: CartWrapperService,
    private modalService: NgbModal,
    private securityService: SecurityService
    ) { }

  ngOnInit(): void {
    if(!this.securityService.IsAuthorized){
      this.securityService.GoToLoginPage();
    }
    if (this.configurationService.isReady) {
      this.loadData();
    } else {
      this.configurationService.settingLoaded$.subscribe(x => {
        this.loadData();
      });
    }
  }

  loadData() {
    let promiseAddress = () => Promise.all([
      this.getAddress(),
      this.service.getAddress().toPromise()
    ]);
    promiseAddress().then(() => this.getCart());
  }

  getCart(): any {
    this.service.getCart().subscribe(cart => {
      this.cart = cart;
      this.calculateTotalPrice();
    });
  }

  getAddress(){
      this.service.getAddress().subscribe({
        next: addressList => {
          this.addressList = addressList;
          this.shippingAddress = this.addressList.length > 0 ? this.addressList[0] : null;
          this.addressJsonString = JSON.stringify(this.shippingAddress);
        }
      });
  }

  itemQuantityChanged(item: ICartItem): void {
    let maximunQuantity: number;
    if (item.quantity < 1) {
      item.quantity = 1;
    }
    this.service.getSkuMappingItem(item).subscribe({
      next: sku => {
        console.log(sku);
        maximunQuantity = sku.available;
      },
      complete: () => {
        console.log('quantity' + maximunQuantity);
        if (maximunQuantity < item.quantity) {
          console.log('smaller');
          item.quantity = maximunQuantity;
        }
        this.calculateTotalPrice();
        this.service.setCart(this.cart).subscribe({
          next: x => console.log('cart updated:', x)
        });
      }
    });
  }

  itemQuantityChangedDown(item: ICartItem) {
    item.quantity = item.quantity - 1;
    this.itemQuantityChanged(item);
  }

  itemQuantityChangedUp(item: ICartItem) {
    item.quantity = item.quantity + 1;
    this.itemQuantityChanged(item);
  }

  // onChangeAddress($event) {
  //   console.log($event.target.value);
  // }

  removeCartItem(event: any, itemRemove: ICartItem) {
    this.service.removeCartItem(itemRemove).subscribe({
      next: x => console.log(x)
    })
    this.getCart();
    this.cartWrapper.updateBadge();
  }

  update(): Observable<boolean> {
    // this.cart.shippingFee = this.shippingFee;
    // this.cart.totalPrice = this.totalPrice;
    const setCartObservable = this.service.setCart(this.cart);
    setCartObservable
      .subscribe(
        x => {
          this.errorMessages = [];
          console.log('cart updated:', x);
        },
        errorMessage => this.errorMessages = errorMessage.messages);
    return setCartObservable;
  }

  checkOut() {
    let addressObj:IAddress = JSON.parse(this.addressJsonString);
    let address = `${addressObj.street}, ${addressObj.address1}, ${addressObj.address2}, ${addressObj.address3}`;
    let isSuccessed = this.service.createOrder(this.cart, this.selectedPayment.value, address);
    isSuccessed.subscribe({
      next: res => { },
      complete: () => {
        this.service.setCartEmpty().subscribe({
          next: (res)=>{
            this.cart = res;
          }
        });
      }
    });
  }

  private calculateTotalPrice() {
    this.totalPrice = 0;
    this.provisional = 0;
    this.shippingFee = 0;
    this.cart.items.forEach(item => {
      this.provisional += (item.itemPrice * item.quantity);
      this.shippingFee += this.shippingAddress.id * 1400;
  });
    this.totalPrice = this.provisional + this.shippingFee;
    this.cart.shippingFee = this.shippingFee;
    this.cart.totalPrice = this.totalPrice
  }

stringify(str: any): string {
  return JSON.stringify(str);
}

confirmCheckout(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' })
    .result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
      console.log(result);
      this.checkOut();
    }, (reason) => {
      if (reason === ModalDismissReasons.ESC) {
        console.log('by pressing ESC');
      } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
        console.log('by clicking on a backdrop');
      } else {
        console.log(`with: ${reason}`);
      }
    });
  }
changePaymentMethod() {
    const modelPayment = this.modalService.open(ModalComponent, { ariaLabelledBy: 'modal-basic-title' })
    modelPayment.componentInstance.model = this.paymentList;
    modelPayment.result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
      console.log(result);
      this.selectedPayment = JSON.parse(result);
    }, (reason) => {
      this.closeResult = `Dismissed ${(reason)}`;
    });
  }
}


