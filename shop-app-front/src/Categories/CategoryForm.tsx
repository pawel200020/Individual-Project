import * as Yup from "yup";
import {Form, Formik, FormikHandlers, FormikHelpers} from "formik";
import TextField from "../Forms/TextField";
import {Link} from "react-router-dom";
import {categoryCreationDTO} from "./Category.model.t";

export default function CategoryForm(props: categoryFormProps) {
    return (<Formik initialValues={props.model}
                    onSubmit={props.onSubmit}
                    validationSchema={Yup.object({
                        name: Yup.string().required('this field is required').max(50,'max length is 50 characters')
                    })
                    }
    >
        {(formikProps) => (
            <Form>
                <TextField field='name' displayName='Name'/>
                <button /*disabled={formikProps.isValid}*/ className='btn btn-primary' type='submit'>submit</button>
                <Link className='btn btn-secondary' to='/Categories/Index'>Cancel</Link>
            </Form>
        )}
    </Formik>);
}
interface  categoryFormProps{
    model: categoryCreationDTO;
    onSubmit(values: categoryCreationDTO, action: FormikHelpers<categoryCreationDTO>): void;
}