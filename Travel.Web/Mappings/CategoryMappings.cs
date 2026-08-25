using AutoMapper;
using Travel.Web.DTOs.CategoryDtos;
using Travel.Web.Entitites;

namespace Travel.Web.Mappings
{
    public class CategoryMappings : Profile
    {
        public CategoryMappings()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, ResultCategoryDto>();
            CreateMap<ResultCategoryDto, UpdateCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
