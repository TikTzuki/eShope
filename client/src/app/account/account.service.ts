import { DataService } from '../shared/services/data.service';
import { ConfigurationService } from '../shared/services/configuration.service';
import { SecurityService } from '../shared/services/security.service';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { IAddress, ICustomer } from '../shared/models/customer.model';
import { Observable } from 'rxjs';
import { IAddressJson1 } from '../shared/models/addressJson.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  accountUrl:string;

  constructor(
    private service: DataService,
    private configurationService: ConfigurationService,
    private sercurityService: SecurityService
  ) {
   if(this.configurationService.isReady){
    this.accountUrl = this.configurationService.serverSettings.purchaseUrl + '/api/customers'
   } else {
     this.configurationService.settingLoaded$.subscribe({
       next: res =>{
         this.accountUrl = this.configurationService.serverSettings.purchaseUrl + '/api/customers';
       }
     })
   }
  }

  userInfo(){
    let url = this.accountUrl + '/' + this.sercurityService.UserData.id;
    console.log(url);
    return this.service.get(url).pipe<ICustomer>(tap((res: any)=>{
      return res;
    }))
  }

  getAddressList(){
    let url = window.location.origin + '/assets/address.json';
    return this.service.get(url).pipe<any>(tap((res: any)=>{
      return res.data;
    }))
  }
}