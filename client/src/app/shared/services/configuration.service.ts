import { IConfiguration } from '../models/configuration.model';
import { Subject } from 'rxjs';
import { StorageService } from './storage.service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {
  serverSettings: IConfiguration;
  private settingsLoadedSource = new Subject();
  settingLoaded$ = this.settingsLoadedSource.asObservable();
  isReady: boolean = false;
  listServer: ListServer = new ListServer();

  constructor(private http: HttpClient, private storageService: StorageService) {
    console.log('constructor');
  }

  load(){
    console.log('load()');
    let baseURI = document.baseURI.endsWith('/') ? document.baseURI : `${document.baseURI}/`;
    //Todo: jus for test
    baseURI = 'http://localhost:3000/';
    let url = `${baseURI}Home/Configuration`;
    this.http.get(url).subscribe((response)=>{
      console.log('server setting loaded');
      this.serverSettings = response as IConfiguration;
      this.storageService.store('identityUrl', this.serverSettings.identityUrl);
      this.storageService.store('purchaseUrl', this.serverSettings.purchaseUrl);
      // this.storageService.store('signalrHubUrl', this.serverSettings.signalrHubUrl);
      // this.storageService.store('activateCampaignDetailFunction', this.serverSettings.activateCampaignDetailFunction);
      this.isReady = true;
      this.settingsLoadedSource.next();
    });
  }
}

class ListServer{
  jsonServer = 'http://localhost:3000/';
  springServer = 'http://localhost:8080/';
  dotnerServer = 'http://localhost:0000/';
}