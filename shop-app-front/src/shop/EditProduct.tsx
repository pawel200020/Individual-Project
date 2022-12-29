import ProductForm from "./ProductForm";
import {categoryDTO} from "../Categories/Category.model.t";

export default function EditProduct(){
    const nonSelectedCategories: categoryDTO[] = [{id: 1, name: "t=shirt"}]
    const selectedCategories: categoryDTO[] = [{id: 2, name: "jeans"}]


    return(
        <>
            <h3>Edit Product</h3>
            <ProductForm model={{name: 'asdf', manufactureDate: new Date('22-11-2000'), pictureUrl: 'https://pesa.pl/content/uploads/2020/09/PZ07764.jpg', caption: "## hello world", isAvailable: true}} onSubmit={values=>console.log(values)}
            selectedCategories={selectedCategories} nonSelectedCategories={nonSelectedCategories}/>
        </>
    )
}