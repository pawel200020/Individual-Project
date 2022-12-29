import {Link, useNavigate} from 'react-router-dom';
import {ErrorMessage, Field, Form, Formik} from "formik";
import * as Yup from 'yup'
import TextField from "../Forms/TextField";
import CategoryForm from "./OrderForm";
import OrderForm from "./OrderForm";

export default function CreateOrder() {
    const navigate = useNavigate();
    return (
        <>
            <h3>Create Order</h3>
            <OrderForm model={{name: '', products: ''}}
                       selectedProducts={[]}
                       onSubmit={(val) => {
                           console.log(val);
                       }
                       }/>

        </>
    )
}