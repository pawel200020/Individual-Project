import {useParams} from "react-router-dom";
import CategoryForm from "./CategoryForm";

export default function EditCategory() {
    const {id}: any = useParams();
    return (
        <>
            <h3>Edit Category</h3>
            <CategoryForm model={{name: ''}}
                          onSubmit={(val) => {
                              console.log(val);
                              console.log(id);
                          }
                          }/>
        </>
    )
}