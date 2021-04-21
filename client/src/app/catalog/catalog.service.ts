import { DataService } from '../shared/services/data.service';
import { ConfigurationService } from '../shared/services/configuration.service';
import { Observable } from 'rxjs';
import { ICatalog } from '../shared/models/catalog.model';
import { IProduct } from '../shared/models/product.model';
import { Injectable } from "@angular/core";
import { tap } from 'rxjs/operators';
import { ICategory } from '../shared/models/category.model';
import { IBrand } from '../shared/models/brand.model';

@Injectable()
export class CatalogService{
  private catalogUrl: string='';
  private categoryUrl: string='';
  private brandUrl: string='';

  constructor(
    private service: DataService, private configurationService: ConfigurationService
  ){
    console.log('catalog service contruct');
    this.configurationService.settingLoaded$.subscribe(x => {
      this.catalogUrl = this.configurationService.serverSettings.purchaseUrl + '/api/catalog';
      this.categoryUrl = this.configurationService.serverSettings.purchaseUrl + '/api/categories';
      this.brandUrl = this.configurationService.serverSettings.purchaseUrl + '/api/brands';
    });
  }
  
  getCatalog(pageIndex: number, pageLimit: number, category: number, brand: string): Observable<ICatalog> {
    let url = this.catalogUrl; 
    console.log('get catalog', url);
    url = url
      + '?page=' + pageIndex
      + '&limit=' + pageLimit;

    if (category){
      url = url + '&categoryId=' + category;
    }

    if (brand){
      url = url + '&brand=' + brand;
    }
    console.log(url);
    return this.service.get(url).pipe<ICatalog>(tap((response: any) => {
      return response;
    }));
  }

  getCategories(): Observable<ICategory[]>{
    return this.service.get(this.categoryUrl).pipe<ICategory[]>(tap((response:any)=>{
      return response;
    }));
  }

  getBrands(): Observable<IBrand[]> {
    return this.service.get(this.brandUrl).pipe<IBrand[]>(tap((response: any) => {
      return response
    }));
  }
}