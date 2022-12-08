import {ProductDTO} from "./Products.model";
import SingleProduct from "./SingleProduct";
import css from "./ProductList.module.css"
import Loading from "../utils/Loading";
import GenericList from "../utils/GenericList";

export default function ProductList(props: ProductListProps) {

    return (<GenericList list={props.products}>
        <div className={css.div}>
            {props.products?.map(product => <SingleProduct {...product} key={product.ID}/>)}
        </div>
    </GenericList>)

}

interface ProductListProps {
    products?: ProductDTO[];
}