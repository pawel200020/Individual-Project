import {Field, Form, Formik} from "formik";
import {categoryDTO} from "../Categories/Category.model.t";
import React, {useEffect, useState} from "react";
import axios, {AxiosResponse} from "axios";
import {urlCategories, urlProducts} from "../endpoints";
import {ProductDTO, ProductDTOIndex} from "./Products.model";
import ProductList from "./ProductList";
import DisplayErrors from "../utils/DisplayError";
import {Navigate, useLocation, useNavigate} from "react-router-dom";
import Pagination from "../utils/Pagination";

export default function FilterProduct() {
    const initVals: filterProductForm = {
        name: "",
        categoryId: -1,
        isAvalible: true,
        page: 1,
        recordsPerPage: 16

    }
    const [errors, setErrors] = useState<string[]>([]);
    const [categories, setCategories] = useState<categoryDTO[]>([]);
    const [products, setProducts] = useState<ProductDTOIndex[]>([]);
    const [totalAmountOfPages, setTotalAmountOfPages] = useState(0);
    const navigate = useNavigate();
    const query = new URLSearchParams(useLocation().search);
    useEffect(() => {
        axios.get(`${urlCategories}/all`).then((response: AxiosResponse<categoryDTO[]>) => {
                console.log("setting data")
                setCategories(response.data);
            }
        )
    }, [])

    useEffect(() => {
        if (query.get('name')) {
            initVals.name = query.get('name') as string;
        }
        if (query.get('categoryId')) {
            initVals.categoryId = parseInt(query.get('categoryId')!, 10);
        }
        if (query.get('isAvalible')) {
            initVals.isAvalible = true
        }
        if (query.get('page')) {
            initVals.page = parseInt(query.get('page')!, 10);
        }
        if (query.get('recordsPerPage')) {
            initVals.recordsPerPage = parseInt(query.get('recordsPerPage')!, 10);
        }

    })

    function searchProducts(values: filterProductForm) {
        console.log("setting 2")
        try {
            modifyUrl(values);

            axios.get(`${urlProducts}/filter`, {params: values}).then((response: AxiosResponse<ProductDTOIndex[]>) => {

                const records = parseInt(response.headers['totalamountofrecords'], 10)
                console.log(records)
                setTotalAmountOfPages(Math.ceil(records / values.recordsPerPage))
                setProducts(response.data);
                setErrors([]);

            }).catch(function (error) {
                if (error.response) {
                    console.log(error.response.data);
                    console.log(error.response.status);
                    console.log(error.response.headers);
                    setErrors(error.response.data);
                }
            });
        } catch (error: any) {
            if (error && error.response) {
                setErrors(error.response.data);
            }
        }

    }

    useEffect(() => {
        searchProducts(initVals)
    }, [])

    function modifyUrl(values: filterProductForm) {
        const queryString: string[] = [];
        if (values.name) {
            queryString.push(`name=${values.name}`)
        }
        if (values.categoryId) {
            queryString.push(`categoryId=${values.categoryId}`)
        }
        if (values.isAvalible) {
            queryString.push(`isAvalible=${values.isAvalible}`)
        }
        queryString.push(`page=${values.page}`)
        navigate(`/Shop/Filter?${queryString.join(('&'))}`)
    }

    return (
        <>
            <DisplayErrors errors={errors}/>
            <h3>filter</h3>
            <Formik initialValues={initVals} onSubmit={values => {
                values.page = 1
                searchProducts(values)
            }
            }>
                {(FormikProps) => (
                    <>
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
                                        <Field className='form-check-input' id="isAvalible" name='isAvalible'
                                               type='checkbox'/>
                                        <label className='form-check-label' htmlFor="isAvalible"> Is product
                                            available</label>
                                    </div>
                                </div>
                                <div className='col-auto'>
                                    <select className='form-select' {...FormikProps.getFieldProps("recordsPerPage")}>
                                        <option>4</option>
                                        <option>8</option>
                                        <option>12</option>
                                        <option>16</option>

                                    </select>
                                </div>
                                <div className="col-auto">
                                    <button className='btn btn-primary' type="button"
                                            onClick={() => FormikProps.submitForm()}>Submit
                                    </button>
                                    <button className='btn btn-danger ms-3'
                                            onClick={() => FormikProps.setValues({
                                                name: "",
                                                categoryId: -1,
                                                isAvalible: true,
                                                page: 1,
                                                recordsPerPage: 16

                                            })}>Reset
                                    </button>
                                </div>
                            </div>
                        </Form>
                        <div className="row">
                        <ProductList products={products}></ProductList>
                        </div>
                        <br/>
                        <Pagination totalAmountOfPages={totalAmountOfPages} onChange={newPage => {
                            FormikProps.values.page = newPage;
                            searchProducts(FormikProps.values)
                        }} currentPage={FormikProps.values.page}/>

                    </>)}
            </Formik>
        </>
    )
}

interface filterProductForm {
    name: string,
    categoryId: number;
    isAvalible: boolean;
    page: number
    recordsPerPage: number
}