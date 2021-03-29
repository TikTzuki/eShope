import { StorageService } from './storage.service';
import { ConfigurationService } from './configuration.service';
import { Subject, Observable } from 'rxjs';
import { HttpClient } from '@microsoft/signalr';
import { HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  // private headers: HttpHeaders;
  private authenticationSource = new Subject<boolean>();
  authenticationChallenge$ = this.authenticationSource.asObservable();
  private authorityUrl='';

  public UserData: any;
  public IsAuthorized!: boolean;
  constructor(
    private headers: HttpHeaders,
    private httpClient: HttpClient,
    private router: Router,
    private configurationService: ConfigurationService,
    private storageService: StorageService
  ) {
    
    this.configurationService.settingLoaded$.subscribe(x=>{
      this.authorityUrl = this.configurationService.serverSettings.identityUrl;
      this.storageService.store('IdentityUrl', this.authorityUrl);
    });
    if(this.storageService.retrieve('IsAuthorized') !=== ''){
      this.IsAuthorized = this.storageService.retrieve('IsAuthorized');
      this.authenticationSource.next(true);
      this.UserData = this.storageService.retrieve('userData');
    }
  }

  public GetToken():any{

  }

  public ResetAuthorizationData(){

  }

  public SetAuthorizationData(token: any, idToken: any){

  }

  public Authorize(){

  }

  public AuthorizedCallBack(){

  }

  public Logoff(){

  }

  public HandleError(error: any){

  }

  private urlBase64Decode(str: string){

  }

  private getDataFromToken(token: any){
    let data = {};

    if( typeof token != 'undefined'){
      
    }
  }

  private getUserData(){ 
  }

  private setHeaders(){
    
  }
}
