import {Link, useNavigate} from 'react-router-dom';
import {ErrorMessage, Field, Form, Formik} from "formik";
import * as Yup from 'yup'
import TextField from "../Forms/TextField";
import CategoryForm from "./OrderForm";
import OrderForm from "./OrderForm";
import {orderCreationDTO} from "./Order.model.t";
import {convertOrderToFormData} from "../utils/formDataUtils";
import axios from "axios";
import {urlOrders} from "../endpoints";
import {useState} from "react";
import DisplayErrors from "../utils/DisplayError";

export default function CreateOrder() {
    const navigate = useNavigate();
    const [errors, setErrors] = useState<string[]>([]);
    async function create(order: orderCreationDTO){
        try{
            setErrors([]);
            console.log(order);
            const formData = convertOrderToFormData(order)
            const response = await axios({
                method: 'post',
                url: urlOrders,
                data: formData,
                headers: {'Content-Type': 'multipart/form-data'}
            })
            navigate(`/Orders/${response.data}`)

        }
        catch(error: any){
            if(error && error.response){
                setErrors(error.response.data);
            }
        }
    }
    return (
        <>
            <h3>Create Order</h3>
            <DisplayErrors errors={errors}/>
            <OrderForm model={{name: ''}}
                       selectedProducts={[]}
                       onSubmit={async val => {
                          await create(val);
                       }
                       }/>

        </>
    )
}