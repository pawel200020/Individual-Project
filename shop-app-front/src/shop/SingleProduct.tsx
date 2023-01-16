import {ProductDTO} from "./Products.model";
import css from './SingleProduct.module.css'
export default  function  SingleProduct(props: ProductDTO){
    const buildLink =() => `shop/${props.id}`
    return(
        <div className={css.div}>
            <a>
                <img alt="Image" src={"./"+props.picture}/>
            </a>
            <p>
                <a href={buildLink()}>name: {props.name}</a>
                <br/>
                <a>price: {props.price}</a>
            </p>
        </div>
    )
}