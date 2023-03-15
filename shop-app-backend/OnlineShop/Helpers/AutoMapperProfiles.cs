using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using OnlineShop.DTO;
using OnlineShop.Entities;

namespace OnlineShop.Helpers
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CategoryCreationDTO, Category>();

            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<OrderCreationDTO, Order>()
                .ForMember(x => x.OrdersProducts, options => options.MapFrom(MapOrderProducts));

            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<ProductCreationDTO, Product>()
                .ForMember(x => x.Picture, options => options.Ignore())
                .ForMember(x => x.ProductsCategories, options => options.MapFrom(MapProductCategories));

            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.Category, options => options.MapFrom(MapProductCategories));

            CreateMap<Order, OrderDTO>()
                .ForMember(x => x.OrdersProducts, options => options.MapFrom(MapOrderProductsOrder));
        }

        private List<ProductsCategories> MapProductCategories(ProductCreationDTO productCreationDto, Product product)
        {
            var result = new List<ProductsCategories>();
            if (productCreationDto.CategoriesIds == null)
            {
                return result;
            }

            foreach (var id in productCreationDto.CategoriesIds)
            {
                result.Add(new ProductsCategories() {CategoryId = id});
            }

            return result;
        }

        private List<OrdersProducts> MapOrderProducts(OrderCreationDTO orderCreationDto, Order order)
        {
            var result = new List<OrdersProducts>();
            if (orderCreationDto.OrdersProducts == null)
            {
                return result;
            }

            foreach (var product in orderCreationDto.OrdersProducts)
            {
                result.Add(new OrdersProducts() {ProductId = product.Id, Quantity = product.Quantity});
            }

            return result;
        }

        private List<CategoryDTO> MapProductCategories(Product product, ProductDTO productDto)
        {
            var res  = new List<CategoryDTO>();
            if (product.ProductsCategories != null)
            {
                foreach (var cat in product.ProductsCategories)
                {
                    res.Add(new CategoryDTO(){Id=cat.CategoryId,Name = cat.Category.Name});
                }
            }

            return res;
        }

        private List<ProductsOrdersDTO> MapOrderProductsOrder(Order order, OrderDTO orderDto)
        {
            var res = new List<ProductsOrdersDTO>();
            if (order.OrdersProducts != null)
            {
                foreach (var product in order.OrdersProducts)
                {
                    res.Add(new ProductsOrdersDTO(){Id = product.ProductId, Name = product.Product.Name, Picture = product.Product.Picture, Quantity = product.Quantity});
                }
            }

            return res;
        }
    }
}
