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
        }
    }
}
