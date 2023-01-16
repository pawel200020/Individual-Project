import ProductForm from "./ProductForm";
import {categoryDTO} from "../Categories/Category.model.t";
import {DomUtil} from "leaflet";
import {ProductCreationDTO, productsPostGetDTO} from "./Products.model";
import {useEffect, useState} from "react";
import DisplayErrors from "../utils/DisplayError";
import {convertProductToFormData} from "../utils/formDataUtils";
import axios, {AxiosResponse} from "axios";
import {urlProducts} from "../endpoints";
import {useNavigate} from "react-router-dom";
import Loading from "../utils/Loading";

export default function CreateProduct(){
    const [nonSelectedCategories, setNonSelectedCategories] = useState<categoryDTO[]>([]);
    const [loading,setLoading] = useState(true);
    useEffect(()=>{
        axios.get(`${urlProducts}/postget`). then((response: AxiosResponse<productsPostGetDTO>)=>{
            setNonSelectedCategories(response.data.categories);
            setLoading(false);
        })
    },[])
    const [errors, setErrors] = useState<string[]>([]);
    const navigation = useNavigate();
    async function create (product: ProductCreationDTO){
        try{
            const formData = convertProductToFormData(product)
            await axios({
                method: 'post',
                url: urlProducts,
                data: formData,
                headers: {'Content-Type': 'multipart/form-data'}
            })
            navigation('/Shop')
        }
        catch (error: any){
            if(error && error.response){
                setErrors(error.response.data);
            }
        }
    }
//const nonSelectedCategories: categoryDTO[] = [{id: 1, name: "t=shirt"},{id: 2, name: "jeans"}]
    return(
        <>
           <h3>Create Product</h3>
            {loading ? <Loading/> :
                <>
                    <DisplayErrors errors={errors}/>
                    <ProductForm model={
                        {name: '', manufactureDate: undefined, isAvailable: true}}
                                 onSubmit={async values=>await create(values)}
                                 nonSelectedCategories={nonSelectedCategories}
                                 selectedCategories={[]}/>
                </>
            }
        </>
    )
}