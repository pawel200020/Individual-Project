
import {userCredentials} from "./auth.model";
import {Form, Formik, FormikHelpers} from "formik";
import * as Yup from 'yup'
import TextField from "../Forms/TextField";
export default function RegisterForm(props: authFormProps){
    return(
        <>
            <Formik
                initialValues={props.model}
                onSubmit={props.onSubmit}
                validationSchema={Yup.object({
                    email: Yup.string().required('This field is required').email('you have to insert a valid email'),
                    password: Yup.string().required('This field is required')
                })}>
                {formikProps =>(
                    <Form>
                        <TextField field='userName' displayName='User name'/>
                        <TextField field='email' displayName='Email'/>
                        <TextField field='phoneNumber' displayName='Phone number'/>
                        <TextField field='password' displayName='Password' type='password'/>
                        <TextField field='passwordConfirm' displayName='Confirm password' type='password'/>
                        <button disabled={formikProps.isSubmitting} type='submit'>Submit</button>
                    </Form>
                )}
            </Formik>
        </>
    )
}
interface authFormProps{
    model: userCredentials;
    onSubmit(values: userCredentials, actions: FormikHelpers<userCredentials>): void;
}