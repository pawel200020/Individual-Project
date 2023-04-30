import {useContext, useEffect, useState} from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import './Ratings.css';
import AuthContext from "../auth/AuthContext";
import Swal from "sweetalert2";

export default function Ratings (props: ratingProps){
    const[maximumValueArr, setMaximumValueArr] = useState<number[]>([]);
    const [selectedValue, setSelectedValue] = useState<number>(props.selectedValue);
    const {claims} = useContext(AuthContext);

    useEffect(()=>{
        setMaximumValueArr(Array(props.maximumValue).fill(0));
    },[props.maximumValue])

    function handleMouseOver(rate: number){
        setSelectedValue(rate);
    }

    function handleMouseLeave(){
        //setSelectedValue(props.selectedValue);
    }

    function handleClick(rate: number){
        const userIsLoggedIn = claims.length > 0;
        if(!userIsLoggedIn){
            Swal.fire({title: 'error', icon: "error", text: 'You need to login'});
            return;
        }
        setSelectedValue(rate);
        props.onChange(rate);
        //window.location.reload();
    }

    return(
        <>
            {maximumValueArr.map((value,index)=> <FontAwesomeIcon
                onMouseOver={()=>handleMouseOver(index+1)}
                onMouseLeave={()=> handleMouseOver(index+1)}
                onClick={()=>handleClick(index+1)}
                icon='star'
                key={index}
                className={`fa-lg pointer ${selectedValue >= index+1 ? 'checked' : null}`}/> )}
        </>
    )
}
interface ratingProps{
    maximumValue: number;
    selectedValue: number;
    onChange(rate: number): void;
}