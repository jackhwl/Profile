using AutoMapper;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesList;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesWithProducts;
using Wenlin.Application.Features.Categories.Queries.GetCategoryDetail;
using Wenlin.Application.Features.Products.Commands.CreateProduct;
using Wenlin.Application.Features.Products.Queries.GetProductDetail;
using Wenlin.Application.Features.Products.Queries.GetProductsList;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<Product, ProductListVm>().ReverseMap();
        CreateMap<Product, ProductDetailVm>().ReverseMap();

        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryListVm>();
        CreateMap<Category, CategoryProductListVm>();
        CreateMap<Category, CategoryDetailVm>();

        CreateMap<Product, CreateProductCommand>().ReverseMap();

    }
}
