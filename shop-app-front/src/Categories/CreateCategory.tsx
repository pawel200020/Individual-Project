import {Link, useNavigate} from 'react-router-dom';
import {ErrorMessage, Field, Form, Formik} from "formik";
import * as Yup from 'yup'
import TextField from "../Forms/TextField";
import CategoryForm from "./CategoryForm";
import {categoryCreationDTO} from "./Category.model.t";
import axios from "axios";
import {urlCategories} from "../endpoints";
import {useState} from "react";
import DisplayErrors from "../utils/DisplayError";
export default function CreateCategory() {
    const navigate = useNavigate();
    const [errors,setErrors] = useState<string[]>([])
    async function create(category: categoryCreationDTO){
        try{
            await axios.post(urlCategories, category);
            navigate('/Categories/Index');
        }
        catch(error: any){
            if(error && error.response){
                setErrors(error.response.data)
            }
        }
    }
    return (
        <>
            <h3>Create Category</h3>
            <DisplayErrors errors={errors}/>
            <CategoryForm model = {{name: ''}}
            onSubmit={ async value =>{
                await create(value);
            }
        }/>

        </>
    )
}