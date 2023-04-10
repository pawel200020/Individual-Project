import './App.css';
import NavBar from "./utils/NavBar";
import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import routes from "./route-config";
import configValidations from './Validations';
import {useContext, useEffect, useState} from "react";
import {claim} from "./auth/auth.model";
import AuthContext from "./auth/AuthContext";
import {Browser} from "leaflet";
import retina = Browser.retina;
import {getClaims} from "./auth/HandleJWT";

function App() {
    const {update, claims} = useContext(AuthContext);
    const [claim, setClaim] = useState<claim[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        setClaim(getClaims());
    }, []);


    useEffect(() => {
        update(getClaims());
    }, [update]);

    useEffect(() => {
        setIsLoading(false);
    }, [claims])

    const isAuthorized = (roles?: string[]): boolean => {
        if (!roles) return true;
        return !!claims.find(claim => claim.name === "role" && roles.includes(claim.value));
    }

    return (
        <>
            <AuthContext.Provider value={{claims: claim, update: setClaim}}>
                <BrowserRouter>
                    <NavBar/>
                    <div className="container">
                        {!isLoading && <Routes>
                            {routes.map(route => (
                                <Route
                                    key={route.path}
                                    path={route.path}
                                    element={isAuthorized(route.roles) ? <route.component/> : <Navigate to="/"/>}
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
