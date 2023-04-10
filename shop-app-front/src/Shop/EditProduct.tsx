import ProductForm from "./ProductForm";
import {useEffect, useState} from "react";
import axios, {AxiosResponse} from "axios";
import {urlProducts} from "../endpoints";
import {Navigate, useNavigate, useParams} from "react-router-dom";
import {ProductCreationDTO, productPutGetDTO} from "./Products.model";
import {convertProductToFormData} from "../utils/formDataUtils";
import DisplayErrors from "../utils/DisplayError";
import Loading from "../utils/Loading";

export default function EditProduct(){
    const {id}: any = useParams();
    const [product, setProduct]= useState<ProductCreationDTO>();
    const [productPutGet, setProductPutGet] = useState<productPutGetDTO>();
    const [errors, setErrors] = useState<string[]>([]);
    const navigate = useNavigate();
    useEffect(()=>{
        axios.get(`${urlProducts}/PutGet/${id}`)
            .then((response: AxiosResponse<productPutGetDTO>)=>{
                const model : ProductCreationDTO ={
                    name: response.data.product.name,
                    price: response.data.product.price,
                    quantity: response.data.product.quantity,
                    IsAvalible: response.data.product.isAvalible,
                    manufactureDate: response.data.product.manufactureDate,
                    pictureUrl: response.data.product.picture,
                    caption: response.data.product.caption
                };
                setProduct(model);
                setProductPutGet(response.data);
            })
    },[id])

    async function edit(productToEdit: ProductCreationDTO){
        try{
            console.log("trying...");
            const formData = convertProductToFormData(productToEdit)
            console.log(formData);
            await axios({
                method: 'put',
                url: `${urlProducts}/${id}`,
                data: formData,
                headers: {'Content-Type': 'multipart/form-data'}
            })
            console.log(formData);
            navigate(`/Shop/${id}`)
        }
        catch (error: any){
            console.log(error)
            if(error && error.response){
                setErrors(error.response.data);
            }
        }
    }

    return(
        <>
            <h3>Edit Product</h3>
            <DisplayErrors errors={errors}/>
            {product && productPutGet ?
                <ProductForm model={product}
                             onSubmit={async val => await edit(val)}
                             selectedCategories={productPutGet?.selectedCategories} nonSelectedCategories={productPutGet?.nonSelectedCategories}/>
            :<Loading/>}

        </>
    )
}