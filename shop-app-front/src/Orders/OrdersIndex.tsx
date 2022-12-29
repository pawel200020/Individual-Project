import {Link} from "react-router-dom";

export default function OrdersIndex(){
    return(
        <>
            <h3>Orders Index</h3>
            <Link className='btn btn-primary' to='/Orders/Create'>Create Order</Link>
        </>
    )
}