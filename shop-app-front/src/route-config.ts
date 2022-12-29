import About from "./About/About";
import Shop from "./shop/Shop";
import Home from "./Home/Home";
import CreateProduct from "./shop/CreateProduct";
import EditProduct from "./shop/EditProduct";
import EditCategory from "./Categories/EditCategory";
import CreateCategory from "./Categories/CreateCategory";
import CategoriesIndex from "./Categories/CategoriesIndex";
import FilterProduct from "./shop/FilterProduct";
import PageNotFound from "./utils/PageNotFound";
import DeleteProduct from "./shop/DeleteProduct";
import OrdersIndex from "./Orders/OrdersIndex";
import CreateOrder from "./Orders/CreateOrder";
import EditOrder from "./Orders/EditOrder";
import DeleteOrder from "./Orders/DeleteOrder";

const routes = [
    {path: '/About', component: About},
    {path: '/Shop', component: Shop},
    {path: '/Shop/Create', component: CreateProduct},
    {path: '/Shop/Edit/:id', component: EditProduct},
    {path: '/Shop/Delete', component: DeleteProduct},
    {path: '/Categories/Edit/:id', component: EditCategory},
    {path: '/Categories/Create', component: CreateCategory},
    {path: '/Categories/Index', component: CategoriesIndex},
    {path: '/Orders/Index', component: OrdersIndex},
    {path: '/Orders/Create', component: CreateOrder},
    {path: '/Orders/Edit/:id', component: EditOrder},
    {path: '/Orders/Delete/:id', component: DeleteOrder},

    {path: '/Shop/Filter', component: FilterProduct},
    {path: '/', component: Home},
    {path: '*', component: PageNotFound}
];
export default routes;