import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details.component';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ProductDetailService } from './productDetails.service';
import { fas } from '@fortawesome/free-solid-svg-icons';

@NgModule({
  imports: [
    BrowserModule,
    SharedModule,
    CommonModule,
    FontAwesomeModule
  ],
  declarations: [ProductDetailsComponent],
  providers: [ProductDetailService]
})
export class ProductDetailsModule{
  constructor(library: FaIconLibrary){
    library.addIconPacks(fas);
  }
}