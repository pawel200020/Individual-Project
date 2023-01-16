import {Link} from "react-router-dom";
import {ProductDTO} from "../shop/Products.model";
import {urlOrders, urlProducts} from "../endpoints";
import IndexEntity from "../utils/IndexEntity";
import React from "react";
import {orderDTO} from "./Order.model.t";

export default function OrdersIndex(){
    return(
        <>
            <IndexEntity<orderDTO> url={urlOrders} entityName="Product" createUrl={'/Orders/Create'} title="Orders">
                {(orders,buttons)=>
                    <>
                        <thead>
                        <tr>
                            <th></th>
                            <th>Name</th>
                        </tr>
                        </thead>
                        <tbody>
                        {orders?.map(product=> <tr key={product.id}>
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