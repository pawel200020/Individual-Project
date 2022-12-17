import {Field, Form, Formik} from "formik";
import {categoryDTO} from "../Categories/Category.model.t";

export default function FilterProduct() {
    const initVals = {
        name: '',
        categoryId: 0,
        isAvailable: true
    }
    const categories: categoryDTO[] = [{id: 1, name: "models"}, {id: 2, name: "tickets"}];
    return (
        <>
            <h3>filter</h3>
            <Formik initialValues={initVals} onSubmit={values => console.log(values)}>
                {(FormikProps) => (
                    <Form>
                        <div className="row gx-3 align-items-center">
                            <div className='col-auto'>
                                <input type="text" className="form-control" id="name"
                                       placeholder="name of a product" {...FormikProps.getFieldProps("name")}/>
                            </div>
                            <div className='col-auto'>
                                <select className='form-select' {...FormikProps.getFieldProps("categoryId")}>
                                    <option value="0">---Choose a category---</option>
                                    {categories.map(category => <option key={category.id}
                                                                        value={category.id}>{category.name}</option>)}
                                </select>
                            </div>
                            <div className='col-auto'>
                                <div className='form-check'>
                                    <Field className='form-check-input' id="isAvailable" name='isAvailable' type='checkbox'/>
                                    <label className='form-check-label' htmlFor="isAvailable"> Is product
                                        available</label>
                                </div>
                            </div>
                            <div className="col-auto">
                                <button className='btn btn-primary' type="button" onClick={() => FormikProps.submitForm()}>Submit
                                </button>
                                <button className='btn btn-danger ms-3'
                                        onClick={() => FormikProps.setValues(initVals)}>Reset
                                </button>
                            </div>
                        </div>
                    </Form>
                )}
            </Formik>
        </>
    )
}

interface filterProduct {
    name: string,
    categoryId: number;
    isAvailable: boolean;
}