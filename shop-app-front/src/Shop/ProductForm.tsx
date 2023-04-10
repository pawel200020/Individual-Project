import {Form, Formik, FormikHelpers} from "formik";
import {ProductCreationDTO} from "./Products.model";
import TextField from "../Forms/TextField";
import {Link} from "react-router-dom";
import * as Yup from 'yup'
import DateField from "../Forms/DateField";
import ImageField from "../Forms/ImageField";
import MarkdownField from "../Forms/MarkdownField";
import CheckboxField from "../Forms/CheckboxField";
import MultipleSelector, {multipleSelectorModel} from "../Forms/MultipleSelector";
import {isValidElement, useState} from "react";
import {categoryDTO} from "../Categories/Category.model.t";
import NumberField from "../Forms/NumberField";
export default function ProductForm(props: productsFormProps) {
    const [selectedCategories, setSelectedCategories ] = useState(mapToModel(props.selectedCategories));
    const [nonSelectedCategories, setNonSelectedCategories ] = useState(mapToModel(props.nonSelectedCategories));

    function mapToModel(items: {id: number, name: string}[]):multipleSelectorModel[]{
        return items.map(item=>{
            return{key: item.id, value: item.name}
        })
    }

    return (<>
        <Formik
            initialValues= {props.model}
            onSubmit={(values,actions)=>{
                values.categoriesIds= selectedCategories.map(item=>item.key)
             props.onSubmit(values, actions)
            }}
            validationSchema={Yup.object({
                // manufactureDate: Yup.date().required("this field is required"),
                name: Yup.string().required("this field is required"),
                quantity: Yup.number().required("this field is required").min(1),
                price: Yup.number().required("this field is required").min(1),
            })}
        >
            {(formikProps)=>(
                <Form>
                    <TextField field="name" displayName="Name"/>
                    <CheckboxField displayName="is Available" field="IsAvalible"/>
                    <NumberField field='price' displayName='Price'/>
                    <NumberField field='quantity' displayName='Quantity'/>
                    {/*<DateField field='manufactureDate' displayName='Manufacture Date'/>*/}
                    <ImageField displayName="Picture" filed="picture" imageUrl={props.model.pictureUrl}/>
                    <MarkdownField displayName="Caption of a product" field="caption"/>
                    <MultipleSelector displayName="Category" selected={selectedCategories} nonSelected={nonSelectedCategories} onChange={(selected,nonSelected)=>{
                        setSelectedCategories(selected)
                        setNonSelectedCategories(nonSelected)
                    }
                    }/>
                    <button className='btn btn-primary' type="submit">Save Changes
                    </button>
                    <Link to="/Shop" className="btn btn-danger">Cancel</Link>
                </Form>
            )}
        </Formik>

    </>)
}
interface productsFormProps {
 model: ProductCreationDTO;
 selectedCategories: categoryDTO[];
 nonSelectedCategories: categoryDTO[];
 onSubmit(values: ProductCreationDTO, action: FormikHelpers<ProductCreationDTO>): void;
}