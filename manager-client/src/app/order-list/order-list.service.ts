import { DataService } from '../shared/services/data.service';
import { ConfigurationService } from '../shared/services/configuration.service';
import { SecurityService } from '../shared/services/security.service';
import { ICatalog } from '../shared/models/catalog.model';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IOrder } from '../shared/models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderListService {
  private orderUrl: string = '';

  constructor(
    private service: DataService,
    private configurationService: ConfigurationService,
    private SecurityService: SecurityService,
  ) {
    if(this.configurationService.isReady){
      this.orderUrl = this.configurationService.serverSettings.purchaseUrl + '/api/orders';
    } else {
      this.configurationService.settingLoaded$.subscribe(x=>{
        this.orderUrl = this.configurationService.serverSettings.purchaseUrl + '/api/orders';
      });
    }
  }

  getOrdersCatalog(params: {[param:string]:any}):Observable<ICatalog<IOrder>>{
    let url = this.orderUrl;
    if (params &&  Object.values(params).some(value=>value)) { 
      url += '?';
      for (const [key, value] of Object.entries(params)) {
        if (value) {
          url += `${key}=${value}&`;
        }
      }
      url = url.substr(0, url.lastIndexOf('&'));
    }
    return this.service.get(url).pipe<ICatalog<IOrder>>(tap((response: any)=>{
      return response;
    }))
  }

  updateOrder(order: IOrder){
    let url = `${this.orderUrl}/${order.id}`;
    return this.service.put(url, order).pipe(tap((response:any)=>{
      return response;
    }))
  }

}