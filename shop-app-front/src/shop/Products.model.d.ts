export interface ProductDTO{
    ID: number;
    title: string;
    price: number;
    image: string;
}
export interface landingPageDTO{
    products?: ProductDTO[];
    premiumProducts?: ProductDTO[];
}