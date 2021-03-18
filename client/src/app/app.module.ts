import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'
import { ToastrModule } from 'ngx-toastr';
import { routes, routing } from './app.routes';
import { SharedModule } from './shared/shared.module';
import { CommonModule } from '@angular/common';
import { CatalogModule } from './catalog/catalog.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProductDetailsModule } from './product-details/productDetails.module';
import { CartModule } from './cart/cart.module';
import { SlickCarouselModule } from 'ngx-slick-carousel';
@NgModule({
  declarations: [		
    AppComponent
   ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    ToastrModule.forRoot(),
    routing,
    HttpClientModule,
    // TODO: T_T
    SharedModule.forRoot(),
    FontAwesomeModule,
    SlickCarouselModule,
    CatalogModule,
    ProductDetailsModule,
    CartModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
