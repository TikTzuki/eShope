import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { SecurityService } from '../../shared/services/security.service';
import { ConfigurationService } from '../../shared/services/configuration.service';
import { ICustomer, IAddress } from '../../shared/models/customer.model';
import { IAddressJson1, IAddressJson2, IAddressJson3 } from '../../shared/models/addressJson.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


interface ICurrentAddress {
  level1_id,
  id,
  customerId,
  street,
  name,
  type,
  level2s,
  address2: IAddressJson2,
  address3: IAddressJson3
}
@Component({
  selector: 'cutomer-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {
  authenticated: boolean;
  authSubscription;
  address: IAddress[];
  currentAddress: ICurrentAddress;
  addressList: IAddressJson1[];
  constructor(
    private configurationService: ConfigurationService,
    private securityService: SecurityService,
    private service: AccountService,
    private modalService: NgbModal) {
    this.authenticated = securityService.IsAuthorized;
  }

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

  getAddress() {
    this.service.getAddress().subscribe({
      next: res => {
        this.address = res;
      }
    });
  }

  deleteAddress(id) {
    this.service.deleteAddress(id);
  }

  loadData() {
    this.getAddress();
  }

  changeStreet($event){
    this.currentAddress.street = $event.target.value;
  };

  changeAddress(addressId) {
    this.address.forEach(address => {
      if (address.id == addressId) {
        this.currentAddress = {
          level1_id: null,
          id: address.id,
          customerId: address.customerId,
          street: address.street,
          name: address.address1,
          type: null,
          level2s: null,
          address2: null,
          address3: null
        }
      }
    });
    this.service.getAddressList().subscribe({
      next: res => {
        this.addressList = res.data;
        console.log(this.addressList);
      }
    })

  }

  createAddress(){
    let address: IAddress = {
      id: null,
      customerId: this.currentAddress.customerId,
      street: this.currentAddress.street,
      address1: this.currentAddress.name,
      address2: this.currentAddress.address2.name,
      address3: this.currentAddress.address3.name,
    }
    console.log(address);
    this.service.createAddress(address).subscribe({
      next: x=> {},
      complete: ()=>{
        this.currentAddress = null;
        this.loadData();
      }
    })
  }

  modifySelectedAddress() {
    let address: IAddress = {
      id: this.currentAddress.id,
      customerId: this.currentAddress.customerId,
      street: this.currentAddress.street,
      address1: this.currentAddress.name,
      address2: this.currentAddress.address2.name,
      address3: this.currentAddress.address3.name,
    }
    this.service.updateAddress(address).subscribe({
      next: x => { },
      complete: () => {
        this.currentAddress = null;
        this.loadData();
      }
    })
  }
  
  open(content, itemId) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      let promiseAddress = () => Promise.all([
        this.service.deleteAddress(itemId).toPromise()
      ]);
      promiseAddress().then(() => this.loadData());
      window.location.reload();
    }, (reason) => {
    });
  }

  cancelAddress() {
    this.currentAddress = null;
  }
  
  changeCurrentAddress1($event) {
    let index = $event.target.value;
    this.currentAddress = {
      ...this.currentAddress,
      ...this.addressList[index],
      address2: this.addressList[index].level2s[0],
      address3: this.addressList[index].level2s[0].level3s[0],
    }
  }

  changeCurrentAddress2($event) {
    this.currentAddress.address2 = this.currentAddress.level2s[$event.target.value];
    console.log(this.currentAddress.address2);
  }

  changeCurrentAddress3($event) {
    this.currentAddress.address3 = this.currentAddress.address2.level3s[$event.target.value];
  }
}
