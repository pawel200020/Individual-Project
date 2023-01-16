import {productsOrderDTO} from "../shop/Products.model";

export interface orderCreationDTO{
    name: string;
    products: productsOrderDTO[];
}
export interface orderDTO{ //future search and filter
    id: number;
    name: string;
}