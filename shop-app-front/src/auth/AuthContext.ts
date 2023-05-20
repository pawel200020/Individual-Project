import React from "react";
import {claim} from "./auth.model";
import {getClaims} from "./HandleJWT";

    const AuthContext = React.createContext<{
    claims: claim[];
    update(claims: claim[]): void
}>({claims: getClaims(), update: (val)=>{}})

export default AuthContext;