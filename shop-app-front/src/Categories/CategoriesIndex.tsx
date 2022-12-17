import {Link} from "react-router-dom";

export default function CategoriesIndex(){
    return(
        <>
            <h3>Categories Index</h3>
            <Link className='btn btn-primary' to='/Categories/Create'>Create Category</Link>
        </>
    )
}