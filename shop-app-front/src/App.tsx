import './App.css';
import NavBar from "./utils/NavBar";
import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import routes from "./route-config";
import {useContext, useEffect, useState} from "react";
import {claim} from "./auth/auth.model";
import AuthContext from "./auth/AuthContext";
import {getClaims} from "./auth/HandleJWT";
import configureInterceptor from "./utils/httpinterceptors";

configureInterceptor();

function App() {
    const {update, claims} = useContext(AuthContext);
    const [claim, setClaim] = useState<claim[]>(getClaims());
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        setClaim(getClaims());
    }, []);

    useEffect(() => {
        update(getClaims());
    }, []);

    useEffect(() => {
        setIsLoading(false);
    }, [claims])

    const isAuthorized = (roles?: string[]): boolean => {
        if (!roles) return true;
        let test = claims.find(claim => claim.name === "role" && roles.includes(claim.value));
        return test!== undefined;
    }

    return (
        <>
            <AuthContext.Provider value={{claims: claim,update: setClaim}}>
                <BrowserRouter>
                    <NavBar/>
                    <div className="container">
                        {!isLoading && <Routes>
                            {routes.map(route => (
                                <Route
                                    key={route.path}
                                    path={route.path}
                                    element={isAuthorized(route.role) ? <route.component/> : <Navigate to="/"/>}
                                />
                            ))}
                        </Routes>}
                    </div>
                </BrowserRouter>
            </AuthContext.Provider>
            <footer className='bd-footer py-3 mt-3 bg-light fixed-bottom'>
                <div className='container'>
                    Online Shop System {new Date().getFullYear().toString()}
                </div>
            </footer>
        </>
    );
}


export default App;
