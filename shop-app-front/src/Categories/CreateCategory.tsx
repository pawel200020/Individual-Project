import {Link, useNavigate} from 'react-router-dom';
import {ErrorMessage, Field, Form, Formik} from "formik";
import * as Yup from 'yup'
import TextField from "../Forms/TextField";
import CategoryForm from "./CategoryForm";
export default function CreateCategory() {
    const navigate = useNavigate();
    return (
        <>
            <h3>Create Category</h3>
            <CategoryForm model = {{name: ''}}
            onSubmit={(val) => {
            console.log(val);
        }
        }/>

        </>
    )
}