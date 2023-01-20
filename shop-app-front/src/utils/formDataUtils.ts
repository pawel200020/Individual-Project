import {ProductCreationDTO} from "../shop/Products.model";
import {orderCreationDTO} from "../Orders/Order.model.t";

export function convertProductToFormData (product: ProductCreationDTO): FormData{
    const formData = new FormData();
    formData.append('name',product.name);
    formData.append('IsAvalible',  JSON.stringify( product.IsAvalible));
    formData.append('quantity',  JSON.stringify( product.quantity));
    formData.append('price',JSON.stringify( product.price))
    if(product.caption){
        formData.append('caption', product.caption)
    }
    if(product.manufactureDate){
        formData.append('manufactureDate', formatDate(product.manufactureDate))
    }
    if(product.picture){
        formData.append('picture',product.picture);
    }
    formData.append('categoriesIds',JSON.stringify(product.categoriesIds));
    return formData;
}

export function convertOrderToFormData(order: orderCreationDTO){
    const formData = new FormData();
    formData.append('name',order.name)
    formData.append('ordersProducts',JSON.stringify(order.products));
    return formData;
}
function formatDate(date: Date){
    date = new Date(date);
    const format = new Intl.DateTimeFormat("en",{
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
    })
    const [
        {value: month},,
        {value: day},,
        {value: year}
    ] = format.formatToParts(date);
    return `${year}-${month}-${day}`;
}