import {productsOrderDTO} from "../shop/Products.model";
import {Typeahead} from "react-bootstrap-typeahead";
import NumberField from "./NumberField";
import {ReactElement} from "react";

export default function TypeAheadProducts(props: typeAheadProductProps) {

    const products: productsOrderDTO[] = [
        {
            id: 1,
            name: "t-shirt",
            quantity: 3,
            picture: "https://img01.ztat.net/article/spp-media-p1/fb9d37c0a73a3de99b1c96124eecebbd/4e9535852a4e4c3fad5f4a01b45fbae7.jpg?imwidth=762"
        },
        {
            id: 2,
            name: "hoodie",
            quantity: 2,
            picture: "https://inrablew.pl/wp-content/uploads/2022/02/INRABLEW-GREY-BOXY-HOODIE.jpg"
        },
        {
            id: 3,
            name: "jeans",
            quantity: 4,
            picture: "https://lp2.hm.com/hmgoepprod?set=format%5Bwebp%5D%2Cquality%5B79%5D%2Csource%5B%2F0b%2F55%2F0b5578deeeca8accdb4df772021df564b4104c95.jpg%5D%2Corigin%5Bdam%5D%2Ccategory%5B%5D%2Ctype%5BDESCRIPTIVESTILLLIFE%5D%2Cres%5Bm%5D%2Chmver%5B2%5D&call=url%5Bfile%3A%2Fproduct%2Fmain%5D"
        }
    ]
    const selected: productsOrderDTO[] = [] //cleanup textbox after click
    return (
        <div className='mb-3'>
            <label>{props.displayName}</label>
            <Typeahead
                id='typeahead'
                onChange={product => {
                    if (props.products.findIndex((x => x.id === product[0].id))===-1)
                        props.onAdd([...props.products, product[0]])
                    console.log(product)
                }}
                options={products}
                labelKey={product => product.name}
                filterBy={['name']}
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