import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShippingComponent } from './shipping/shipping.component';
import { CatalogComponent } from './catalog/catalog.component';
import { RouterModule } from '@angular/router';
import { Routes } from "@angular/router";
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountComponent } from './account/account.component';
import { AddressComponent } from './account/address/address.component';
import { ProfileComponent as ProfileComponent } from './account/info/info.component';
import { OrderComponent } from './account/order/order.component';

export const routes: Routes = [
  { path: '', redirectTo: 'catalog', pathMatch: 'full'},
  { path: 'products/:productId', component: ProductDetailsComponent },
  { path: 'cart', component: CartComponent },
  { path: 'shipping', component: ShippingComponent },
  {path: 'catalog', component: CatalogComponent},
  {path: 'sign-in', component: LoginComponent},
  {path: 'sign-up', component: RegisterComponent},
  {path: 'account', component: AccountComponent, children: [
    {path: 'address', component: AddressComponent},
    {path: 'profile', component: ProfileComponent},
    {path: 'orders', component: OrderComponent}
  ]},
  {path: 'address', component: AddressComponent}
];

export const routing = RouterModule.forRoot(routes);