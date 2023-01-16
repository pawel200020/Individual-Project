import {ProductDTO} from "./Products.model";
import SingleProduct from "./SingleProduct";
import css from "./ProductList.module.css"
import GenericList from "../utils/GenericList";
import {Link} from "react-router-dom";

export default function ProductList(props: ProductListProps) {

    return (
        <>
            <Link className='btn btn-primary' to='/Shop/Create'>Create Product</Link>
            <GenericList list={props.products}>
                <div className={css.div}>
                    {props.products?.map(product => <SingleProduct {...product} key={product.id}/>)}
                </div>
            </GenericList>
        </>)

}

interface ProductListProps {
    products?: ProductDTO[];
}