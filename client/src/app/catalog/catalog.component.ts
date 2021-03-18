import { ICategory } from '../shared/models/category.model';
import { ICatalog } from '../shared/models/catalog.model';
import { IPager } from '../shared/models/pager.model';
import { Subscription, throwError, Observable, of } from 'rxjs';
import { CatalogService } from './catalog.service';
import { ConfigurationService } from '../shared/services/configuration.service';
import { SecurityService } from '../shared/services/security.service';
import { Component, OnInit } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { IProduct } from '../shared/models/product.model';
import { ISku } from '../shared/models/sku.model';
import { CartWrapperService } from '../shared/services/cart.wrapper.service';
import { IBrand } from '../shared/models/brand.model';
import { faArrowDown } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  brands!: IBrand[];
  categories!: ICategory[];
  categorySelected: number;
  brandSelected: string;
  catalog: ICatalog;
  paginationInfo: IPager;
  authenticated: boolean = false;
  authSubscription: Subscription;
  errorRecieved: boolean;
  //Fontawesome
  constructor(
    private service: CatalogService,
    private configurationService: ConfigurationService,
    private securityService: SecurityService,
    // private basketService: BasketWrapperService
  ) {
    this.authenticated = securityService.IsAuthorized;
  }

  ngOnInit(): void {

    if (this.configurationService.isReady) {
      this.loadData();
    }
    else {
      this.configurationService.settingLoaded$.subscribe(x => {
        this.loadData();
      });
    }
    this.authSubscription = this.securityService.authenticationChallenge$.subscribe(res => {
      this.authenticated = res;
    });
  }

  loadData(){
    this.getCategories();
    this.getBrands();
    this.getCatalog(0, 2);
  }

  getCategories(){
    this.service.getCategories().subscribe(categories =>{
      this.categories = categories;
      // let allCategories = {id:null, name: 'category name from angular'}
      // this.categories.unshift(allCategories);
    })
  }

  getBrands(){
    this.service.getBrands().subscribe(brands =>{
      this.brands = brands;
    })
  }

  onFilterApplied(event: any){
    // event.preventDefault();

    // this.categorySelected = this.categorySelected && this.categorySelected.toString() != null ? this.categorySelected : null;
    // this.brandSelected = this.brandSelected && this.brandSelected.toString() != null ? this.brandSelected : null;
    console.log(this.categorySelected);
    console.log(this.brandSelected);
    this.getCatalog(this.paginationInfo.itemsPage, this.paginationInfo.actualPage, this.categorySelected, this.brandSelected);
  }

  onBrandFilterChanged(event: any, value: string){
    // event.preventDefault();
    console.log(value);    
    this.brandSelected = value;
  }

  onCategoryFilterChanged(event: any, value: number){
    // event.preventDefault();
    console.log(value);
    this.categorySelected = value;
  }

  onPageChanged(value: any){
    this.paginationInfo.actualPage = value;
    this.getCatalog(value ,this.paginationInfo.itemsPage);
  }

  // addToCart(item: ISku){
  //   this.basketService.addItemToBasket(item);
  // }

  getCatalog(pageIndex: number, pageLimit: number, category?: number, brand?:string){
    this.errorRecieved = false;
    
    this.service.getCatalog(pageIndex, pageLimit, category, brand)
      .pipe(catchError( err => this.handleError(err) ))
      .subscribe(catalog => {
        this.catalog = catalog;
        console.log(catalog);
        this.paginationInfo = {
          actualPage : catalog.pageIndex,
          itemsPage : catalog.pageSize,
          totalItems: catalog.count,
          items: catalog.pageSize,
          totalPages: Math.ceil(catalog.count / catalog.pageSize)
        }
        console.log(this.paginationInfo);
        
      })
  }

  private handleError(error: any){
    this.errorRecieved = true;
    if (error.error instanceof ErrorEvent) {
      console.log('Client side network error occurred', error.error.message);
    } else {
        console.error('Backend - ' +
          `status: ${error.status}, ` +
          `statusText: ${error.statusText}, ` +
          `message: ${error.error.message}`);
    }
    return throwError(error);
  }

}
