import {Link} from "react-router-dom";
import {ProductDTOIndex} from "../shop/Products.model";
import {urlOrders, urlProducts} from "../endpoints";
import IndexEntity2 from "../utils/IndexEntity2";
import React from "react";
import {orderDTOIndex} from "./Order.model.t";

export default function OrdersIndex(){
    return(
        <>
            <IndexEntity2<orderDTOIndex> url={urlOrders} entityName="Order" createUrl={'/Orders/Create'} title="Orders">
                {(orders,buttons)=>
                    <>
                        <thead>
                        <tr>
                            <th></th>
                            <th>Name</th>
                        </tr>
                        </thead>
                        <tbody>
                        {orders?.map(order=> <tr key={order.id}>
                            <td>
                                {buttons(`Shop/Edit/${order.id}`,order.id)}
                            </td>

                            <td>
                                <Link to={`/Orders/${order.id}`}>{order.name}</Link>
                            </td>
                        </tr>)}
                        </tbody>
                    </>}
            </IndexEntity2>
        </>
    )
}