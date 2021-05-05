import { DataService } from '../shared/services/data.service';
import { ConfigurationService } from '../shared/services/configuration.service';
import { SecurityService } from '../shared/services/security.service';
import { OrderListService } from '../order-list/order-list.service';
import { EOrderStatus } from '../shared/models/orderStatus.const';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ICatalog } from '../shared/models/catalog.model';
import { IOrder } from '../shared/models/order.model';
import { Observable, combineLatest, observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  orderUrl:string;
  orders:IOrder[];
  constructor(
    private service: DataService,
    private configurationService: ConfigurationService,
    private securityService: SecurityService
  ) {
    if (this.configurationService.isReady) {
      this.orderUrl = this.configurationService.serverSettings.purchaseUrl + '/api/orders';
    } else {
      this.configurationService.settingLoaded$.subscribe(x => {
        this.orderUrl = this.configurationService.serverSettings.purchaseUrl + '/api/orders';
      });
    }
  }

  getRevenue(dates: Date[]): Observable<any[]> {
    let url = `${this.orderUrl}?pageIndex=0&pageLimit=1000`;
    let observables = [];
    dates.forEach(date => {
      url += `&createDate=${date.toISOString()}`;
      let observable = this.service.get(url)
        .pipe<number>(map((response: any) => {
          let orders: IOrder[] = response.data
          let totalRevenueDay:number = 0;
          orders.forEach(order => {
            totalRevenueDay += order.totalPrice;
          })
          return totalRevenueDay;
        }))
       observables.push(observable); 
        url = url.replace(/&createDate.+/, '');
    })
    return combineLatest(observables);
  }

  getOrderAnalysis(dates: Date[]): Observable<any[]> {
    let url = `${this.orderUrl}?pageIndex=0&pageLimit=1000`;
    let observables = [];
    dates.forEach(date => {
      url += `&createDate=${date.toISOString()}`;
      let observable = this.service.get(url)
        .pipe<number>(map((response: any) => {
          let orders: IOrder[] = response.data;
          return orders.length;
        }))
      observables.push(observable);
        url = url.replace(/&createDate.+/, '');
    })
    return combineLatest(observables);
  }

  getCustomerAnalysis(dates: Date[]):Observable<any[]>{
    let url = `${this.orderUrl}?pageIndex=0&pageLimit=1000`;
    let observables = [];
    dates.forEach(date => {
      url += `&createDate=${date.toISOString()}`;
      let observable = this.service.get(url)
      .pipe<number>(map((response: any)=>{
        let orders: IOrder[] = response.data;
        let customerCount = [];
        orders.forEach(order=>{
          if(!customerCount.includes(order.customerId)){
            customerCount.push(order.customerId);
          }
        })
        return customerCount.length;
      }));
      observables.push(observable);
      url = url.replace(/&createDate.+/, '');
    })
    return combineLatest(observables);
  }
}