import {categoryDTO} from "../Categories/Category.model.t";

export interface ProductDTOIndex {
    id: number;
    name: string;
    isAvailable: boolean;
    price: number;
    manufactureDate?: Date;
    quantity: number;
    caption?: string;
    picture: string;
}

export interface ProductDTO{
    id: number;
    name: string;
    price?: number;
    quantity?: number;
    isAvalible: boolean;
    manufactureDate?: Date;
    picture?: string;
    pictureUrl?: string;
    caption?: string;
    category?: categoryDTO[];
    userVote: number;
    averageVote: number;
}
export interface ProductCreationDTO{
    name: string;
    price?: number;
    quantity?: number;
    IsAvalible: boolean;
    manufactureDate?: Date;
    picture?: File;
    pictureUrl?: string;
    caption?: string;
    categoriesIds?: number[];
}
export interface landingPageDTO{
    products?: ProductDTOIndex[];
    premiumProducts?: ProductDTOIndex[];
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

export interface productPutGetDTO{
    product: ProductDTO;
    selectedCategories: categoryDTO[];
    nonSelectedCategories: categoryDTO[];
}