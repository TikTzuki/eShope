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
  private userNmae: string = '';

  constructor(private service: SecurityService, private signalrService: SignalrService) { }

  ngOnInit(): void {
    this.subscription = this.service.authenticationChallenge$.subscribe(res=>{
      this.authenticated = res;
      this.userNmae = this.service.UserData.email;
    });

    if(window.location.hash){
      this.service.AuthorizedCallBack();
    }

    console.log('identity component, checking authorized' + this.service.IsAuthorized);
    this.authenticated = this.service.IsAuthorized;

    if(this.authenticated){
      if(this.service.UserData){
        this.userNmae = this.service.UserData.email;
      }
    }
  }

  logoutClicked(event: any){
    event.preventDefault();
    console.log('Logout clicked');
    this.logout();
  }

  login(){
    this.service.Authorize();
  }

  logout(){
    this.signalrService.stop();
    this.service.Logoff();
  }
}
