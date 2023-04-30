import {authenticationResponse, userCredentials} from "./auth.model";
import axios from "axios";
import {urlAccounts} from "../endpoints";
import {useContext, useState} from "react";
import DisplayErrors from "../utils/DisplayError";
import AuthForm from "./AuthForm";
import {getClaims, saveToken} from "./HandleJWT";
import AuthContext from "./AuthContext";
import {useNavigate} from "react-router-dom";

export default function  Register(){
    const [errors, setErrors] = useState<string[]>([])
    const {update} = useContext(AuthContext);
    const navigation = useNavigate();
    async function register(credentials: userCredentials){
        try{
            setErrors([]);
            const response = await axios.post<authenticationResponse>(`${urlAccounts}/create`, credentials)
            saveToken(response.data);
            update(getClaims());
            console.log(response);
            navigation('/');
        }
        catch (error: any){
            if(error || error.response){
                setErrors(error.response.data);
            }
        }
    }
    return(
        <>
            <h3>Register</h3>
            <DisplayErrors errors={errors}/>
            <AuthForm model={{email:'', password: ''}} onSubmit={async values => await register(values)}/>
        </>
    )
}