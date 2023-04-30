import {authenticationResponse, claim} from "./auth.model";
const tokenKey = 'token'
const expirationKey = 'token-expiration'
export function saveToken(authData: authenticationResponse){
    localStorage.setItem(tokenKey,authData.token);
    localStorage.setItem(expirationKey,authData.expirationDate.toString());
}
export function getClaims(): claim []{
    const token = localStorage.getItem(tokenKey);
    if(!token){
        logout();
        return [];
    }

    const expiration = localStorage.getItem(expirationKey)!;
    const expirationDate = new Date(expiration);

    if(expirationDate <= new Date()){
        return [];
    }
    const dataToken = JSON.parse(atob(token.split('.')[1]));
    const response: claim [] = [];
    for(const property in dataToken){
        response.push ({name: property, value: dataToken[property]});
    }
    return response;
}
export function logout(){
    localStorage.removeItem(tokenKey);
    localStorage.removeItem(expirationKey);
}

export function getToken(){
    return localStorage.getItem(tokenKey);
}