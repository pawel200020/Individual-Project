import {productsOrderDTO} from "../Shop/Products.model";

export interface orderCreationDTO{
    name: string;
    products?: productsOrderDTO[];
}
export interface orderDTOIndex { //future search and filter
    id: number;
    name: string;
}
export interface orderDTO{
    id: number;
    name: string;
    value: number;
    ordersProducts: productsOrderDTO[];

}