export interface ProductDTO{
    ID: number;
    title: string;
    price: number;
    quantity: number;
    image: string;
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