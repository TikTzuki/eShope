import { DataService } from '../shared/services/data.service';
import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs/operators';
import { ISellerAccount } from '../shared/models/seller.model';
import { SecurityService } from '../shared/services/security.service';
import { ConfigurationService } from '../shared/services/configuration.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  selectedTab: string = '';
  user:ISellerAccount;
  constructor(
    private dataService: DataService,
    private configurationService:ConfigurationService,
    private securityService: SecurityService
  ) { }

  ngOnInit() {
    this.user = this.securityService.UserData;
    console.log(this.user);
    
  }

  changeTab(tab:string){
    this.selectedTab = tab;
  }
}
