import {ProductDTO, ProductDTOIndex} from "./Products.model";
import SingleProduct from "./SingleProduct";
import css from "./ProductList.module.css"
import GenericList from "../utils/GenericList";
import {Link} from "react-router-dom";
import Authorized from "../auth/Authorized";

export default function ProductList(props: ProductListProps) {

    return (
        <>
            <Authorized
                authorized={<>OK</>}
                notAuthorized={<>fail</>}
                role="admin"
            />
            {/*<Link className='btn btn-primary' to='/Shop/Create'>Create Product</Link>*/}
            <GenericList list={props.products}>
                <div className={css.div}>
                    {props.products?.map(product => <SingleProduct {...product} key={product.id}/>)}`
                </div>
            </GenericList>
        </>)

}

interface ProductListProps {
    products?: ProductDTOIndex[];}