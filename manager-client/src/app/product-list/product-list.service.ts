import { DataService } from '../shared/services/data.service';
import { ConfigurationService } from '../shared/services/configuration.service';
import { Injectable } from '@angular/core';
import { ICatalog } from '../shared/models/catalog.model';
import { ObjectUnsubscribedError, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { SecurityService } from '../shared/services/security.service';
import { StorageService } from '../shared/services/storage.service';
import { IProduct } from '../shared/models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductListService {
  private productUrl: string = '';

  constructor(
    private service: DataService,
    private configurationService: ConfigurationService,
    private securityService: SecurityService,
    private storageService: StorageService
  ) {
    if (this.configurationService.isReady) {
      this.productUrl = this.configurationService.serverSettings.purchaseUrl + '/api/products'
    } else {
      this.configurationService.settingLoaded$.subscribe(x => {
        this.productUrl = this.configurationService.serverSettings.purchaseUrl + '/api/products'
      });
    }

  }

  getCatalog(params?: { [param: string]: any }): Observable<ICatalog<IProduct>> {
    let url = this.productUrl + (params ? '?' : '');
    for (const [key, value] of Object.entries(params)) {
      if (value) {
        url += `${key}=${value}&`;
      }
    }
    url = url.substring(0, url.lastIndexOf('&'));
    console.log(url);
    return this.service.get(url).pipe<ICatalog<IProduct>>(tap((response: any) => {
      return response;
    }));
  }

  deleteProduct(productId: number): void {
    let url = this.productUrl;
    url += '/' + productId;
    this.service.delete(url);
  }

}