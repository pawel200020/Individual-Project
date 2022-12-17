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
        </BrowserRouter>

    )
}

export default App;
