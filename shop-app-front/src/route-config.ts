import About from "./About/About";
import Shop from "./shop/Shop";
import Home from "./Home/Home";
import CreateProduct from "./shop/CreateProduct";
import EditProduct from "./shop/EditProduct";
import EditCategory from "./Categories/EditCategory";
import CreateCategory from "./Categories/CreateCategory";
import CategoriesIndex from "./Categories/CategoriesIndex";
import ProductIndex from "./Products/ProductIndex";
import FilterProduct from "./shop/FilterProduct";
import PageNotFound from "./utils/PageNotFound";

const routes = [
    {path: '/About', component: About},
    {path: '/Shop', component: Shop},
    {path: '/Shop/Create', component: CreateProduct},
    {path: '/Shop/Edit', component: EditProduct},
    {path: '/Categories/Edit/:id', component: EditCategory},
    {path: '/Categories/Create', component: CreateCategory},
    {path: '/Categories/Index', component: CategoriesIndex},
    {path: '/Product/Index', component: ProductIndex},
    {path: '/Product/Create', component: CreateProduct},
    {path: '/Product/Edit', component: EditProduct},
    {path: '/Shop/Filter', component: FilterProduct},
    {path: '/', component: Home},
    {path: '*', component: PageNotFound}
];
export default routes;