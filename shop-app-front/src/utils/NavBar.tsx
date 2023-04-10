import {Link, NavLink} from "react-router-dom";
import Authorized from "../auth/Authorized";
import {logout} from "../auth/HandleJWT";
import {useContext} from "react";
import AuthContext from "../auth/AuthContext";

export default function NavBar() {
    const {update, claims} = useContext(AuthContext);
    function getUserEmail(): string{
        return claims.filter(x => x.name === 'email')[0]?.value;
    }
    return (
        <>
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                <div className="container-fluid">
                    <NavLink className="navbar-brand" to="/">Online Shop</NavLink>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                            data-bs-target="#navbarColor03" aria-controls="navbarColor03" aria-expanded="false"
                            aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarColor03"
                         style={{display: 'flex', justifyContent: 'space-between'}}>
                        <ul className="navbar-nav me-auto">
                            <li className="nav-item">
                                <NavLink className="nav-link active" to="/">Home
                                    <span className="visually-hidden">(current)</span>
                                </NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link" to="/Shop/Filter">Shop</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link" to="/News">News</NavLink>
                            </li>
                            <Authorized
                                authorized={<>
                                    <li className="nav-item dropdown">
                                        <a className="nav-link dropdown-toggle" data-bs-toggle="dropdown" href="#"
                                           role="button"
                                           aria-haspopup="true" aria-expanded="false">Administration</a>
                                        <div className="dropdown-menu">
                                            <a className="dropdown-item" href="/Categories/Index">Categories</a>
                                            <a className="dropdown-item" href="/Orders">Orders</a>
                                            <a className="dropdown-item" href="/Shop">Products</a>
                                            <div className="dropdown-divider"></div>
                                            <a className="dropdown-item" href="#">System settings</a>
                                            <div className="dropdown-divider"></div>
                                            <a className="dropdown-item" href="#">Users</a>
                                        </div>
                                    </li>
                                </>}/>

                            <li className="nav-item">
                                <NavLink className="nav-link" to="/About">About</NavLink>
                            </li>
                        </ul>
                        <div className='d-flex'>
                            <Authorized
                                authorized={<>
                                    <span className='nav-link'>Hello {getUserEmail()}</span>
                                    <button className='nav-link btn btn-link' onClick={()=>{
                                        logout();
                                        update([]);
                                    }
                                    }>Logout</button>
                                </>}
                                notAuthorized={<>
                                    <ul className="navbar-nav me-auto">
                                        <li className="nav-item">
                                            <NavLink to="/register" className='nav-link'>Register</NavLink>
                                        </li>
                                        <li className="nav-item">
                                            <NavLink to="/login" className='nav-link'>Login</NavLink>
                                        </li>
                                    </ul>
                                </>}/>
                        </div>
                        <form className="d-flex">
                            <input className="form-control me-sm-2" type="text" placeholder="Search"/>
                            <button className="btn btn-secondary my-2 my-sm-0" type="submit">Search</button>
                        </form>
                    </div>
                </div>
            </nav>
        </>
    )
}