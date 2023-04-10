import {ProductDTO, ProductDTOIndex} from "./Products.model";
import css from './SingleProduct.module.css'
import {Link} from "react-router-dom";
import axios from "axios";
import {urlProducts} from "../endpoints";
import React, {useContext} from "react";
import AlertContext from "../utils/AlertContext";
import customConfirm from "../utils/customCofirm";
import Authorized from "../auth/Authorized";

export default function SingleProduct(props: ProductDTOIndex) {
    const buildLink = () => `/Shop/${props.id}`
    const customAlert = useContext(AlertContext);

    function deleteProduct() {
        axios.delete(`${urlProducts}/${props.id}`)
            .then(() => {
                customAlert();
            })
    }

    return (
        <>
            <div className="col-sm-3">
                <div className="card mb-3 card text-center">
                    <h4 className="card-header">{props.name}</h4>
                    <svg xmlns="http://www.w3.org/2000/svg" className="d-block user-select-none" width="100%"
                         height="200"
                         aria-label="Placeholder: Image cap" focusable="false" role="img"
                         preserveAspectRatio="xMidYMid slice" viewBox="0 0 318 180"
                         style={{
                             fontSize: '1.125rem',
                             textAnchor: 'middle'
                         }}>
                        <rect width="100%" height="100%" fill="#ffffff"></rect>
                        <text x="50%" y="50%" fill="#dee2e6" dy=".3em">Image cap</text>
                        <image xlinkHref={props.picture} x="0" y="0" width="100%" height="100%"/>
                    </svg>
                    <div className="card-body">
                        <p className="card-text">short description</p>
                    </div>
                    <ul className="list-group list-group-flush">
                        <li className="list-group-item">price: {props.price} PLN</li>
                    </ul>
                    <div className="card-body">
                        <Authorized
                            authorized={<>
                                <Link style={{marginRight: '1rem'}} className={"btn btn-info"}
                                      to={`/Shop/Edit/${props.id}`}>Edit</Link>
                            </>}/>
                        <Link style={{marginRight: '1rem'}} className={"btn btn-success"}
                              to={buildLink()}>Details</Link>
                        <Authorized
                            authorized={<>
                                <button onClick={() => customConfirm(() => deleteProduct())}
                                        className='btn btn-danger'>Delete
                                </button>
                            </>}/>

                    </div>
                    <div className="card-footer text-muted">
                        Added:
                    </div>
                </div>
            </div>

        </>
    )
}