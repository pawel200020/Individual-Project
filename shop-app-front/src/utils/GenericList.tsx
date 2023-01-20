import {ReactElement} from "react";
import Loading from "./Loading";
export default  function GenericList (props: genericListProps){
    if(!props.list){
        if(props.loadingUI){
            return props.loadingUI
        }
        else{
            return <Loading/>
        }
    }
    else if(props.list.length==0){
        if(props.emptyListUI){
            return props.emptyListUI;
        }
        else{
            return <><i>There is not any product corresponding to your search criteria</i></>
        }
    }
    else{
        return props.children;
    }

}
interface genericListProps{
    list: any;
    loadingUI?: ReactElement;
    emptyListUI?: ReactElement
    children: ReactElement
}