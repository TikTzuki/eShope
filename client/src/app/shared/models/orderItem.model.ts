import { IImage } from './image.model';
export interface IOrderItem{
  id: number;
  order_id: number;
  sku_id: number;
  name: string;
  variation: string;
  itemPrice: number;
  quantity: number;
  image: string;
}