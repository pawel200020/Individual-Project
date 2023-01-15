
import CategoryForm from "./CategoryForm";
import EditEntity from "../utils/EditEntity";
import {categoryCreationDTO, categoryDTO} from "./Category.model.t";
import {urlCategories} from "../endpoints";

export default function EditCategory() {
    return (
        <>
           <EditEntity<categoryCreationDTO,categoryDTO> entityName="Categories" url={urlCategories} indexUrl='/Categories/Index'>
               {(entity, edit) =>
                    <CategoryForm model={entity} onSubmit={async value =>{await edit(value);}}/>
           }</EditEntity>
        </>
    )
}