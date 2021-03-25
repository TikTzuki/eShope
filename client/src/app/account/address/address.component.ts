import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { SecurityService } from '../../shared/services/security.service';
import { ConfigurationService } from '../../shared/services/configuration.service';
import { ICustomer, IAddress } from '../../shared/models/customer.model';
import { IAddressJson1 } from '../../shared/models/addressJson.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';



@Component({
  selector: 'cutomer-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {
  authenticated: boolean;
  authSubscription;
  customer: ICustomer;
  selectedAddress: IAddress;
  currentAddress: IAddressJson1;
  addressList:IAddressJson1[];
  constructor(
    private configurationService: ConfigurationService,
    private securityService: SecurityService,
    private service: AccountService,
    private modalService: NgbModal) {
      this.authenticated = securityService.IsAuthorized;
  }

  ngOnInit() {
    if(this.configurationService.isReady){
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

  getAddress() {
    this.service.userInfo().subscribe({
      next: res => {
        this.customer = res;
        console.log(this.customer);

      }
    });
  }

  deleteAddress(id){

  }
  loadData(){
    this.getAddress();
  }

modifyAddress(content) {
  this.customer.address.forEach(address=>{
    if(address.id == content){
      this.selectedAddress = address;
      console.log(this.selectedAddress);
      this.service.getAddressList().subscribe({
        next:res => {
          console.log(res.data);
        }
      })
    }
  })
  }

  changeCurrentAddress($event){
    console.log($event.value);
  }
}
