import axios from "axios";
import {getToken} from "../auth/HandleJWT";

export default function configureInterceptor(){
    axios.interceptors.request.use(
        function (config){
            const token = getToken();

            if(token){
                config.headers.authorization = `bearer ${token}`;
            }
            return config;
        },
        function (error){
            return Promise.reject(error);
        }
    )
}