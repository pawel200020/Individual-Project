import ProductList from "./ProductList";
import products, {landingPageDTO, ProductDTO} from './Products.model';
import React, {useEffect, useState} from "react";
import IndexEntity from "../utils/IndexEntity";
import {urlProducts} from "../endpoints";

export default function Shop() {
    const [products, setProducts] = useState<landingPageDTO>({});
    //(() => {
        //const timerId = setTimeout(() => {
        //     setProducts({
        //         products: [{
        //             ID: 1,
        //             name: "t-shirt",
        //             price: 99.58,
        //             picture: "shirt.jpg",
        //             quantity: 11
        //         }, {
        //             ID: 2,
        //             name: "Jeans",
        //             price: 21.37,
        //             picture: "shirt.jpg",
        //             quantity: 11
        //         }
        //         ],
        //         premiumProducts: [{
        //             ID: 1,
        //             name: "t-shirt",
        //             price: 99.58,
        //             picture: "shirt.jpg",
        //             quantity: 13
        //         }, {
        //             ID: 2,
        //             name: "Jeans",
        //             price: 21.37,
        //             picture: "shirt.jpg",
        //             quantity: 16
        //         }
        //         ]
        //     })
        // }, 4000)
    //})
    return (
        <>
            {/*<h2>Product List</h2>*/}
            {/*<ProductList products={products.products}/>*/}
            {/*<h2>Premium Product List</h2>*/}
            {/*<ProductList products={products.premiumProducts}/>*/}
            <IndexEntity<ProductDTO> url={urlProducts} entityName="Product" createUrl={'/Shop/Create'} title="Products">
                {(products,buttons)=>
                    <>
                        <thead>
                            <tr>
                                <th></th>
                                <th>Name</th>
                            </tr>
                        </thead>
                        <tbody>
                        {products?.map(product=> <tr key={product.id}>
                            <td>
                                {buttons(`Shop/Edit/${product.id}`,product.id)}
                            </td>
                            <td>
                                {product.name}
                            </td>
                        </tr>)}
                        </tbody>
                    </>}
            </IndexEntity>

        </>

    )
}