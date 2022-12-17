import ProductList from "./ProductList";
import products, {landingPageDTO} from './Products.model';
import React, {useEffect, useState} from "react";

export default function Shop() {
    const [products, setProducts] = useState<landingPageDTO>({});
    useEffect(() => {
        const timerId = setTimeout(() => {
            setProducts({
                products: [{
                    ID: 1,
                    title: "t-shirt",
                    price: 99.58,
                    image: "shirt.jpg"
                }, {
                    ID: 2,
                    title: "Jeans",
                    price: 21.37,
                    image: "shirt.jpg"
                }
                ],
                premiumProducts: [{
                    ID: 1,
                    title: "t-shirt",
                    price: 99.58,
                    image: "shirt.jpg"
                }, {
                    ID: 2,
                    title: "Jeans",
                    price: 21.37,
                    image: "shirt.jpg"
                }
                ]
            })
        }, 4000)
    })
    return (
        <>
            <h2>Product List</h2>
            <ProductList products={products.products}/>
            <h2>Premium Product List</h2>
            <ProductList products={products.premiumProducts}/>
        </>

    )
}