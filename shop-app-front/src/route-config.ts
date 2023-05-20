import About from "./About/About";
import Shop from "./Shop/Shop";
import Home from "./Home/Home";
import CreateProduct from "./Shop/CreateProduct";
import EditProduct from "./Shop/EditProduct";
import EditCategory from "./Categories/EditCategory";
import CreateCategory from "./Categories/CreateCategory";
import CategoriesIndex from "./Categories/CategoriesIndex";
import FilterProduct from "./Shop/FilterProduct";
import PageNotFound from "./utils/PageNotFound";
import DeleteProduct from "./Shop/DeleteProduct";
import OrdersIndex from "./Orders/OrdersIndex";
import CreateOrder from "./Orders/CreateOrder";
import EditOrder from "./Orders/EditOrder";
import DeleteOrder from "./Orders/DeleteOrder";
import ProductDetails from "./Shop/ProductDetails";
import OrderDetails from "./Orders/OrderDetails";
import Register from "./auth/Register";
import Login from "./auth/Login";

const routes = [
    {path: '/About', component: About},
    {path: '/Shop', component: Shop},
    {path: '/Shop/Create', component: CreateProduct},
    {path: '/Shop/Edit/:id', component: EditProduct},
    {path: '/Shop/:id', component: ProductDetails},
    {path: '/Shop/Delete', component: DeleteProduct},
    {path: '/Categories/Edit/:id', component: EditCategory, role: ["admin"]},
    {path: '/Categories/Create', component: CreateCategory, role: ["admin"]},
    {path: '/Categories/Index', component: CategoriesIndex, role: ["admin"]},
    {path: '/Orders', component: OrdersIndex},
    {path: '/Orders/Create', component: CreateOrder},
    {path: '/Orders/Edit/:id', component: EditOrder},
    {path: '/Orders/Delete/:id', component: DeleteOrder},
    {path: '/Orders/:id', component: OrderDetails},

    {path: '/register', component: Register},
    {path: '/login', component: Login},

    {path: '/Shop/Filter', component: FilterProduct},
    {path: '/', component: Home},
    {path: '*', component: PageNotFound}
];
export default routes;