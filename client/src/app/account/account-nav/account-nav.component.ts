import { Component, OnInit } from '@angular/core';
import { max } from 'rxjs/operators';
import { AddressComponent } from '../address/address.component';
import { OrderComponent } from '../order/order.component';
import { ProfileComponent } from '../info/info.component';

interface navItem {
  name:string;
  link: any;
}
@Component({
  selector: 'account-nav',
  templateUrl: './account-nav.component.html',
  styleUrls: ['./account-nav.component.scss']
})
export class AccountNavComponent implements OnInit {
  navList:navItem[] = [
    {name: 'Address list', link: 'address'},
    {name: 'Profile', link: 'profile'},
    {name: 'Orders', link:'orders'}
  ];
  selectedLink = this.navList[0].link;
  constructor() {

  }
  ngOnInit(): void {

  }
  onClick(link){
    this.selectedLink = link;
  }
}
