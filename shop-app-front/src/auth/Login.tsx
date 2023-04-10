import AuthForm from "./AuthForm";
import {authenticationResponse, userCredentials} from "./auth.model";
import axios from "axios";
import {urlAccounts} from "../endpoints";
import {useContext, useState} from "react";
import DisplayErrors from "../utils/DisplayError";
import {getClaims, saveToken} from "./HandleJWT";
import AuthContext from "./AuthContext";

export default function Login(){
    const [errors, setErrors] = useState<string[]>([]);
    const {update} = useContext(AuthContext);
    async function login (credentials: userCredentials){
        try{
            setErrors([]);
            const response = await axios.post<authenticationResponse>(`${urlAccounts}/login`, credentials);
            saveToken(response.data);
            update(getClaims());
            console.log(response.data)
        }
        catch (error: any){
            if(error || error.response){
                setErrors(error.response.data);
            }
        }

    }

    return(
        <>
            <h3>Login</h3>
            <DisplayErrors errors={errors}/>
            <AuthForm
                model={{email: '', password: ''}}
                onSubmit={async values =>await login(values)}/>
        </>
    )
}