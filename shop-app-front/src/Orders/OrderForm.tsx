import * as Yup from "yup";
import {Field, Form, Formik, FormikHandlers, FormikHelpers} from "formik";
import TextField from "../Forms/TextField";
import {Link} from "react-router-dom";
import {orderCreationDTO} from "./Order.model.t";
import TypeAheadProducts from "../Forms/TypeAheadProducts";
import {useState} from "react";
import {productsOrderDTO} from "../Shop/Products.model";
import {Simulate} from "react-dom/test-utils";
import input = Simulate.input;

export default function OrderForm(props: orderFormProps) {
    const [selectedProduct, setSelectedProduct] = useState(props.selectedProducts);
    return (<Formik initialValues={props.model}
                    onSubmit={(values, actions)=>{
                        values.products = selectedProduct;
                        props.onSubmit(values,actions);
                    }}
                    validationSchema={Yup.object({
                        name: Yup.string().required('this field is required')
                    })
                    }
    >
        {(formikProps) => (
            <Form>
                <TextField field='name' displayName='Name'/>
                <TypeAheadProducts displayName="Products" products={selectedProduct}
                                   onAdd={product => setSelectedProduct(product)} listUI={(product: productsOrderDTO) =>
                    <>
                        {product.name}
                        <input placeholder='Quantity' type='number' value={product.quantity}
                                              onChange={e => {
                                                  const index = selectedProduct.findIndex(x => x.id === product.id)
                                                  const products = [...selectedProduct]
                                                  products[index].quantity = parseInt(e.currentTarget.value);
                                                  setSelectedProduct(products)

                                              }
                                              }/>
                    </>
                }
                onRemove={product => {
                    const products = selectedProduct.filter(x=>x !== product)
                    setSelectedProduct(products);
                }
                }/>
                <button /*disabled={formikProps.isValid}*/ className='btn btn-primary' type='submit'>submit</button>
                <Link className='btn btn-secondary' to='/Categories/Index'>Cancel</Link>
            </Form>
        )}
    </Formik>);
}

interface orderFormProps {
    model: orderCreationDTO;
    selectedProducts: productsOrderDTO[];

    onSubmit(values: orderCreationDTO, action: FormikHelpers<orderCreationDTO>): void;
}