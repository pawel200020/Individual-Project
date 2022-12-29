import {useParams} from "react-router-dom";
import CategoryForm from "./OrderForm";

export default function EditOrder() {
    const {id}: any = useParams();
    return (
        <>
            <h3>Edit Category</h3>
            <CategoryForm model={{name: '', products:'' }}
                          selectedProducts={[]}
                          onSubmit={(val) => {
                              console.log(val);
                              console.log(id);
                          }
                          }/>
        </>
    )
}