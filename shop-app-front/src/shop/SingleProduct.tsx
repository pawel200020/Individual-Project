import {ProductDTO} from "./Products.model";
import css from './SingleProduct.module.css'
export default  function  SingleProduct(props: ProductDTO){
    const buildLink =() => `shop/${props.ID}`
    return(
        <div className={css.div}>
            <a>
                <img alt="Image" src={"./"+props.image}/>
            </a>
            <p>
                <a href={buildLink()}>name: {props.title}</a>
                <br/>
                <a>price: {props.price}</a>
            </p>
        </div>
    )
}