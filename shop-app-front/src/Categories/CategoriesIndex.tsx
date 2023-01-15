import {categoryDTO} from "./Category.model.t";
import {urlCategories} from "../endpoints";
import IndexEntity from "../utils/IndexEntity";

export default function CategoriesIndex() {
    return (
        <>
            <IndexEntity<categoryDTO> url={urlCategories} entityName="Categories" createUrl='/Categories/Create'
                                      title='Categories Index'>
                {(categories, buttons) =>
                    <>
                        <thead>
                        <tr>
                            <th></th>
                            <th>Name</th>
                        </tr>
                        </thead>
                        <tbody>
                        {categories?.map(category =>
                            <tr key={category.id}>
                                <td>
                                    {buttons(`/Categories/Edit/${category.id}`, category.id)}
                                </td>
                                <td>
                                    {category.name}
                                </td>
                            </tr>)}
                        </tbody>
                    </>}
            </IndexEntity>
        </>
    )
}