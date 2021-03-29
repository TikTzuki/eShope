import { ConfigurationService } from '../../shared/services/configuration.service';
import { SecurityService } from '../../shared/services/security.service';
import { AccountService } from '../account.service';
import { Subscription } from 'rxjs';
import { IOrder } from '../../shared/models/order.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  authSubscription: Subscription;
  authenticated: boolean;
  selectedTab = 1;
  orders: IOrder[];
  constructor(
    private configurationService: ConfigurationService,
    private securityService: SecurityService,
    private service: AccountService,
    private modalService: NgbModal
  ) { }

  ngOnInit() {
    if (this.configurationService.isReady) {
      this.loadData();
    } else {
      this.configurationService.settingLoaded$.subscribe(x => {
        this.loadData();
      })
    }

    this.authSubscription = this.securityService.authenticationChallenge$.subscribe({
      next: res => {
        this.authenticated = res;
      }
    })
  }

  changeTab(tab:number){
    this.selectedTab = tab;
    switch (tab) {
      case 1:
        this.loadOrders();
        break;
      case 2:
        this.loadOrders('pending');
        break;
      case 3:
        this.loadOrders('shipping');
        break;
      case 4:
        this.loadOrders('delivered');
        break;
      case 5:
        this.loadOrders('canceled');
        break;
      default:
      break;
    }
  }

  loadData(){
    console.log('load daata');
    this.loadOrders();
  }

  loadOrders(status?:string){
    this.service.getOrders(status).subscribe({
      next: orders=>{
        this.orders = orders;
        console.log(this.orders);
      }
    })
  }

  cancelOrder(orderId: number){
    let order = this.orders.find(order => order.id === orderId);
    order.status = 'canceled';
    console.log(order);
    this.service.updateOrder(order).subscribe({
      next: res=>{},
      complete: ()=>{this.loadData();}
    })
  }

  openConfirmModal(content, orderId) {
    this.modalService.open(content, { ariaLabelledBy: 'change-pwd' }).result.then((result) => {
      this.cancelOrder(orderId);
    }, (reason) => {
    });
  }
}
