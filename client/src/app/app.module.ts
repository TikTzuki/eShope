import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { routes, routing } from './app.routes';
import { SharedModule } from './shared/shared.module';
import { CommonModule } from '@angular/common';
import { CatalogModule } from './catalog/catalog.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProductDetailsModule } from './product-details/productDetails.module';
import { CartModule } from './cart/cart.module';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { LoginComponent } from './login/login.component';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
@NgModule({
  declarations: [
    AppComponent,
      LoginComponent
   ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    NgbModule,
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
   constructor(library: FaIconLibrary){
    library.addIconPacks(fas);
   }
}
