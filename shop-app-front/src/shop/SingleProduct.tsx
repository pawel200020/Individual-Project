import {ProductDTO, ProductDTOIndex} from "./Products.model";
import css from './SingleProduct.module.css'
import {Link} from "react-router-dom";
import customConfirm from "../utils/customCofirm";
import axios from "axios";
export default  function  SingleProduct(props: ProductDTOIndex){
    const buildLink =() => `/Shop/${props.id}`
    return(
        <div className={css.div}>
            <a>
                <img alt="Image" src={props.picture}/>
            </a>
            <p>
                <a href={buildLink()}>name: {props.name}</a>
                <br/>
                <a>price: {props.price}</a>
            </p>
        </div>
    )
}