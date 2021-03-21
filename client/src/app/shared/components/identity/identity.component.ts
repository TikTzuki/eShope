import { Subscription } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../../services/security.service';
import { SignalrService } from '../../services/signalr.service';

@Component({
  selector: 'app-identity',
  templateUrl: './identity.component.html',
  styleUrls: ['./identity.component.css']
})
export class Identity implements OnInit {
  authenticated: boolean = false;
  private subscription!: Subscription;
  private userNane: string = '';

  constructor(private service: SecurityService, private signalrService: SignalrService) { }

  ngOnInit(): void {
    this.subscription = this.service.authenticationChallenge$.subscribe(res => {
      this.authenticated = res;
      this.userNane = this.service.UserData.email;
    });

    if (window.location.hash) {
      this.service.AuthorizedCallBack();
    }

    console.log('checking authorized' + this.service.IsAuthorized);
    this.authenticated = this.service.IsAuthorized;

    if (this.authenticated) {
      if (this.service.UserData) {
        this.userNane = this.service.UserData.email;
      }
    }
  }

  logoutClicked(event: any): void {
    event.preventDefault();
    console.log('Logout clicked');
    this.logout();
  }

  login(): void{
    this.service.Authorize();
  }

  logout(): void{
    this.signalrService.stop();
    this.service.Logoff();
  }
}
