import { IImage } from './image.model';
import { ISku } from './sku.model';
export interface IOrderItem{
  id: number;
  order_id: number;
  sku_id: number;
  name: string;
  variation: string;
  itemPrice: number;
  quantity: number;
  image: string;
  sku: ISku;
}