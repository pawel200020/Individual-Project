import {ErrorMessage, Field} from "formik";

export default function NumberField(props: FieldProps){
    return(
        <>
            <div className='mb-3'>
                <label htmlFor={props.field}>{props.displayName}</label>
                <Field name={props.field} className='form-control'  id='name' type='number'/>
                <ErrorMessage name={props.field}>{msg=><div className='text-danger'>{msg}</div>}</ErrorMessage>
            </div>
        </>
    )
}
interface FieldProps{
    field: string;
    displayName: string;
}