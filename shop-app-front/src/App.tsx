import React, {useEffect, useState} from 'react';
import './App.css';
import {landingPageDTO, ProductDTO} from "./shop/Products.model";
import ProductList from "./shop/ProductList";
import Button from "./utils/Button";

function App() {
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
        <div className='container'>
            <Button>Test</Button>
            <h2>Product List</h2>
            <ProductList products={products.products}/>
            <h2>Premium Product List</h2>
            <ProductList products={products.premiumProducts}/>
        </div>
    )
}

export default App;
