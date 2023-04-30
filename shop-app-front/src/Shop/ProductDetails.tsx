import {useEffect, useState} from "react";
import axios, {AxiosResponse} from "axios";
import {urlProducts, urlRatings} from "../endpoints";
import {Link, useParams} from "react-router-dom";
import {ProductDTO} from "./Products.model";
import Loading from "../utils/Loading";
import ReactMarkdown from "react-markdown";
import Ratings from "../utils/Ratings";
import Swal from "sweetalert2";

export default function ProductDetails(){
    const {id} : any = useParams();
    const [product,setProduct] = useState<ProductDTO>();
    useEffect(()=>{
        axios.get(`${urlProducts}/${id}`)
            .then((response: AxiosResponse<ProductDTO>)=>{
                //response.data.manufactureDate = new Date(response.data.manufactureDate);
                setProduct(response.data);
            })
    },[id])
    console.log(product);

    function handleRate(rate: number){
        axios.post(urlRatings, {rating: rate, productId: id}).then(()=>{
            Swal.fire({icon: "success", title: "Rating received"})
        })
    }

    return(
        <>
            {product ? <div>
            <h2>{product?.name}</h2>
                {product.category?.map(category=>
                    <Link key ={category.id} style ={{marginRight: '5px'}} className="btn btn-primary btn-sm rounded-pill" to ={`/Shop/filter?categoryId=${category.id}`}>{category.name}</Link>
                )}
                Your rating: <Ratings maximumValue={5} selectedValue={product.userVote} onChange={handleRate}/>
                Average rating: {product.averageVote}
                <div style ={{ display: 'flex', marginTop: '1rem'}}>
                    <span style = {{display: 'inline-block', marginRight: '1rem'}}>
                        <img src={product.picture} style={{width: '225px'}} alt="poster"/>
                    </span>
                </div>
                {product.isAvalible  === false ?
                    <div> <p style={{borderWidth: '3px', borderStyle: 'solid', borderColor: '#FF0000', padding: '1em', marginTop: '1em'}}>This product is not currently available</p></div> : null}
                {product.caption ? <div style={{marginTop: '1rem'}}>
                    <h5>Description</h5>
                    <div>
                        <ReactMarkdown>{product.caption}</ReactMarkdown>
                    </div>
                </div>: null}
                <div>
                    <p><b>Quantity:</b> {product.quantity}</p>
                </div>
                <div>
                    <p><b>Price:</b> {product.price} PLN</p>
                </div>
            </div> : <Loading/>}
        </>
    )
}