import React from "react";
import {claim} from "./auth.model";

    const AuthContext = React.createContext<{
    claims: claim[];
    update(claims: claim[]): void
}>({claims: [/*{name: 'role', value: 'admin'}*/], update: ()=>{}})

export default AuthContext;