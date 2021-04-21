import { ConfigurationService } from '../shared/services/configuration.service';
import { SecurityService } from '../shared/services/security.service';
import { Subscription, throwError } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ProductListService } from './product-list.service';
import { ICatalog } from '../shared/models/catalog.model';
import { IPager } from '../shared/models/pager.model';
import { IProduct } from '../shared/models/product.model';
import { EProductStatus } from '../shared/models/productStatus.const';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

interface Product extends IProduct{
  totalAvailable: number,
  minPrice: number,
  maxPrice: number,
}

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  authenticated: boolean = false;
  authSubscription: Subscription;
  errorRecieved: boolean
  // selectedTab: number = 1;
  selectedTab: string = "all";
  EProductStatus = EProductStatus;
  catalog: ICatalog<IProduct>;
  productList: Product[] = [];
  paginationInfo: IPager;
  constructor(
    private configurationService:ConfigurationService,
    private securityService: SecurityService,
    private service: ProductListService,
    private modalService: NgbModal
  ) { 

  }

  ngOnInit() {
    if(this.configurationService.isReady){
      this.loadData();
    } else {
      this.configurationService.settingLoaded$.subscribe(x=>{
        this.loadData();
      });
    }

    this.authSubscription = this.securityService.authenticationChallenge$.subscribe(res=>{
      this.authenticated = res;
    })
  }

  changeTab(status: string) {
    this.selectedTab = status;
    this.getCatalog({
      pageIndex: this.paginationInfo.actualPage,
      pageSize: this.paginationInfo.itemsPage,
      status: status
    });
  }

  loadData(){
    this.getCatalog({pageIndex: 0, pageSize: 10});
  }

  getCatalog(params: {[param:string]:any} ){
    this.errorRecieved = false;
    
    this.service.getCatalog(params).subscribe({
      next: catalog => {
        this.catalog = catalog;
          this.productList = [];
        //Load list product
        this.catalog.data.forEach(product => {
          let productTemp:Product={
            ...product,
            totalAvailable: 0,
            minPrice: Number.MAX_SAFE_INTEGER,
            maxPrice: 0
          }
          product.skus.forEach(sku=>{
            productTemp.totalAvailable += sku.available;
            if(productTemp.minPrice>sku.price){
              productTemp.minPrice = sku.price;
            }
            if(productTemp.maxPrice<sku.price){
              productTemp.maxPrice = sku.price;
            }
          })
          this.productList.push(productTemp);
        });
        // Load page info
        this.paginationInfo = {
          actualPage : catalog.pageIndex,
          itemsPage : catalog.pageSize,
          totalItems: catalog.count,
          items: catalog.pageSize,
          totalPages: Math.ceil(catalog.count / catalog.pageSize)
        }
      },
      error: err=>{
        this.handleError(err);
      }
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


  private deleteProduct(content, itemId) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      let promiseAddress = () => Promise.all([
        this.service.deleteProduct(itemId)
      ]);
      promiseAddress().then(() => this.loadData());
      window.location.reload();
    }, (reason) => {
    });
  }

  get Object(){
    return window.Object;
  }
}
