import products, {landingPageDTO, ProductDTOIndex} from './Products.model';
import React, {useEffect, useState} from "react";
import IndexEntity from "../utils/IndexEntity";
import {urlProducts} from "../endpoints";
import {Link} from "react-router-dom";

export default function Shop() {
    const [products, setProducts] = useState<landingPageDTO>({});
    useEffect(()=>{

    },[])
    return (
        <>
            {/*<h2>Product List</h2>*/}
            {/*<ProductList products={products.products}/>*/}
            {/*<h2>Premium Product List</h2>*/}
            {/*<ProductList products={products.premiumProducts}/>*/}
            <IndexEntity<ProductDTOIndex> url={urlProducts} entityName="Product" createUrl={'/Shop/Create'} title="Products">
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
                                {buttons(`/Shop/Edit/${product.id}`,product.id)}
                            </td>
                            <td>
                                <Link to={`/Shop/${product.id}`}>{product.name}</Link>
                            </td>
                        </tr>)}
                        </tbody>
                    </>}
            </IndexEntity>

        </>

    )
}