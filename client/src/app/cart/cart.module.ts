import { CartComponent } from './cart.component';
import { CartStatusComponent } from './cart-status/cart-status.component';
import { CartService } from './cart.service';
import { ModuleWithProviders, NgModule } from "@angular/core";
import { SharedModule } from '../shared/shared.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';

@NgModule({
  imports: [SharedModule, FontAwesomeModule],
  declarations: [CartComponent, CartStatusComponent],
  providers: [CartService],
  exports: [CartStatusComponent ]
})
export class CartModule{
   constructor(library: FaIconLibrary){
    library.addIconPacks(fas);
   }

  static forRoot(): ModuleWithProviders<CartModule>{
    return {
      ngModule: CartModule,
      providers: [
        CartService
      ]
    }
  }
}