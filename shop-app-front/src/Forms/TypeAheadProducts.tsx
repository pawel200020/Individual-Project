import {productsOrderDTO} from "../Shop/Products.model";
import {AsyncTypeahead, Typeahead} from "react-bootstrap-typeahead";
import NumberField from "./NumberField";
import {ReactElement, useState} from "react";
import axios, {AxiosResponse} from "axios";
import {urlProducts} from "../endpoints";

export default function TypeAheadProducts(props: typeAheadProductProps) {

    const selected: productsOrderDTO[] = [] //cleanup textbox after click
    const [products,setProducts] = useState<productsOrderDTO[]>([])
    const [isLoading, setIsLoading] = useState(false);
    function handleSearch(query: string){
        setIsLoading(true)
        axios.get(`${urlProducts}/searchByName/${query}`)
            .then((response: AxiosResponse<productsOrderDTO[]>)=>{
                setProducts(response.data);
                setIsLoading(false);
            })
    }
    return (
        <div className='mb-3'>
            <label>{props.displayName}</label>
            <AsyncTypeahead
                id='typeahead'
                onChange={product => {
                    if (props.products.findIndex((x => x.id === product[0].id))===-1){
                        products[0].quantity = 1
                        props.onAdd([...props.products, product[0]])
                    }

                }}
                options={products}
                labelKey={product => product.name}
                filterBy={()=> true}
                isLoading={isLoading}
                onSearch={handleSearch}
                placeholder={"Write a product name"}
                minLength={1}
                flip={true}
                selected={selected}
                renderMenuItemChildren={product => (
                    <>
                        <img alt='product' src={product.picture}
                             style={{
                                 height: '64px',
                                 marginRight: '10px',
                                 width: '64px'
                             }}/>
                        <span>{product.name}</span>
                    </>
                )}
            />
            <ul className='list-group'>
                {props.products.map(product=> <li key={product.id} className='list-group-item list-group-item-action'>
                    {props.listUI(product)}
                    <span className='badge badge-primary badge-pill pointer text-dark' style={{marginLeft: '0.5rem'}} onClick={()=> props.onRemove(product)}>X
                    </span>
                </li>)}
            </ul>
        </div>
    )
}

interface typeAheadProductProps {
    displayName: string;
    products: productsOrderDTO[];
    onAdd(products: productsOrderDTO[]): void;
    onRemove(product: productsOrderDTO): void;
    listUI(product: productsOrderDTO): ReactElement
}