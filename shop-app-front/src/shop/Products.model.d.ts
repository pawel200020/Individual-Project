import {categoryDTO} from "../Categories/Category.model.t";

export interface ProductDTO{
    id: number;
    name: string;
    isAvailable: boolean;
    price: number;
    manufactureDate?: Date;
    quantity: number;
    caption?: string;
    picture: string;
}
export interface ProductCreationDTO{
    name: string;
    price?: number;
    quantity?: number;
    isAvailable: boolean;
    manufactureDate?: Date;
    picture?: File;
    pictureUrl?: string;
    caption?: string;
    categoriesIds?: number[];
}
export interface landingPageDTO{
    products?: ProductDTO[];
    premiumProducts?: ProductDTO[];
}

export interface productsOrderDTO{
    id: number;
    name: string;
    quantity: number;
    picture: string;

}
export interface productsPostGetDTO{
    categories: categoryDTO[];
}