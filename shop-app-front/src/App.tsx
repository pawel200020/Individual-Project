import './App.css';
import NavBar from "./utils/NavBar";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import routes from "./route-config";
import configValidations from './Validations';
configValidations();
function App() {

    return (
        <BrowserRouter>
            <NavBar/>
            <div className='container'>
                <Routes>
                    {routes.map(route =>
                        <Route
                            key={route.path}
                            path={route.path}
                            element={<route.component/>}
                        />)
                    }
                </Routes>

            </div>
            {/*<footer className='bd-footer py-3 mt-3 bg-light fixed-bottom'>*/}
            {/*    <div className='container'>*/}
            {/*        Online Shop System {new Date().getFullYear().toString()}*/}
            {/*    </div>*/}
            {/*</footer>*/}
        </BrowserRouter>

    )
}

export default App;
