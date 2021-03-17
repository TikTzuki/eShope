import { IProduct } from './product.model';

export interface ICatalog {
  pageIndex: number;
  data: IProduct[];
  pageSize: number;
  count: number;
}