import { Subscription } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../../services/security.service';
import { ICustomer } from '../../models/customer.model';

@Component({
  selector: 'app-identity',
  templateUrl: './identity.component.html',
  styleUrls: ['./identity.component.scss']
})
export class Identity implements OnInit {
  authenticated: boolean = false;
  private subscription!: Subscription;
  private customer: ICustomer;

  constructor(private service: SecurityService,
    //  private signalrService: SignalrService
     ) { }

  ngOnInit(): void {
    this.subscription = this.service.authenticationChallenge$.subscribe(res => {
      this.authenticated = res;
      this.customer = this.service.UserData.email;
    });

    // if (window.location.hash) {
    //   this.service.AuthorizedCallBack();
    // }

    console.log('checking authorized' + this.service.IsAuthorized);
    this.authenticated = this.service.IsAuthorized;

    if (this.authenticated) {
      if (this.service.UserData) {
        this.customer = this.service.UserData;
      }
    }
  }

  login(): void{
    this.service.Authorize({phone:'', password:''});
  }

  logout(): void{
    this.service.Logoff();
  }
}
