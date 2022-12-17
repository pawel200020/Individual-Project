import {Link} from "react-router-dom";

export default function ProductIndex(){
    return(
        <>
            <h3>Product Index</h3>
            <Link className='btn btn-primary' to='/Product/Create'>Create Product</Link>
        </>
    )
}