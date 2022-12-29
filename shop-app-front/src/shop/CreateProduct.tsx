import ProductForm from "./ProductForm";
import {categoryDTO} from "../Categories/Category.model.t";
import {DomUtil} from "leaflet";

export default function CreateProduct(){
const nonSelectedCategories: categoryDTO[] = [{id: 1, name: "t=shirt"},{id: 2, name: "jeans"}]
    return(
        <>
           <h3>Create Product</h3>
            <ProductForm model={
                {name: '', manufactureDate: undefined, isAvailable: true}}
                         onSubmit={values=>console.log(values)}
                         nonSelectedCategories={nonSelectedCategories}
                         selectedCategories={[]}/>
        </>
    )
}