import {ReactElement, useContext, useEffect, useState} from "react";
import authContext from "./AuthContext";

export default function Authorized(props: authProps){
    const [isAuthorized, setIsAuthorized] = useState(false);
    const {claims} = useContext(authContext);

    useEffect(()=>{
        if(props.role){
            const index = claims.findIndex(claim=> claim.name ==='role' && claim.value === props.role)
            setIsAuthorized(index >-1);
        }
        else{
            setIsAuthorized(claims.length >0);
        }
    },[claims,props.role])
    return(
        <>
            {isAuthorized ? props.authorized : props.notAuthorized}
        </>
    )
}
interface authProps{
    authorized: ReactElement;
    notAuthorized?: ReactElement;
    role?: string;
}