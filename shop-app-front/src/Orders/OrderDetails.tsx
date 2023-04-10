import {Link, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {ProductDTO} from "../Shop/Products.model";
import axios, {AxiosResponse} from "axios";
import {urlOrders, urlProducts} from "../endpoints";
import ReactMarkdown from "react-markdown";
import Loading from "../utils/Loading";
import {orderDTO} from "./Order.model.t";

export default function OrderDetails(){
    const {id} : any = useParams();
    const [order,setOrder] = useState<orderDTO>();
    useEffect(()=>{
        axios.get(`${urlOrders}/${id}`)
            .then((response: AxiosResponse<orderDTO>)=>{
                //response.data.manufactureDate = new Date(response.data.manufactureDate);
                setOrder(response.data);
            })
    },[id])
    console.log(order);
    return(
        <>
            {order ? <div>
                <h2>{order?.name}</h2>

                <ul className='list-group'>
                    {order.ordersProducts?.map(product=>
                        <li key={product.id} className='list-group-item list-group-item-action'>
                                <span style = {{display: 'inline-block', marginRight: '1rem'}}>
                                <img src={product.picture} style={{width: '100px'}} alt="poster"/>
                            </span>
                            {product.name} | <b>{product.quantity}</b>
                        </li>
                    )}
                </ul>

                <ul className='list-group' style={{marginTop:'2rem'}}>
                        <li className='list-group-item list-group-item-action'>
                            Total value of ordered products <b>{order.value} PLN</b>
                        </li>
                </ul>

            </div> : <Loading/>}
        </>
    )
}

