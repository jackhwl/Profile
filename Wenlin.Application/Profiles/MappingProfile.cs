using AutoMapper;
using Wenlin.Application.Features.Categories.Commands.CreateCategory;
using Wenlin.Application.Features.Categories.Commands.UpdateCategory;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesList;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesWithProducts;
using Wenlin.Application.Features.Categories.Queries.GetCategoryCollection;
using Wenlin.Application.Features.Categories.Queries.GetCategoryDetail;
using Wenlin.Application.Features.Customers.Commands.CreateCustomer;
using Wenlin.Application.Features.Customers.Commands.CreateCustomerWithDateOfDeath;
using Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Application.Features.Images.Commands.CreateImage;
using Wenlin.Application.Features.Images.Commands.UpdateImage;
using Wenlin.Application.Features.Images.Queries.GetImageDetail;
using Wenlin.Application.Features.Images.Queries.GetImagesList;
using Wenlin.Application.Features.Products.Commands.CreateProduct;
using Wenlin.Application.Features.Products.Commands.PartiallyUpdateProduct;
using Wenlin.Application.Features.Products.Commands.UpdateProduct;
using Wenlin.Application.Features.Products.Queries.GetProductDetail;
using Wenlin.Application.Features.Products.Queries.GetProductsExport;
using Wenlin.Application.Features.Products.Queries.GetProductsList;
using Wenlin.Application.Helpers;
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
        CreateMap<Category, CreateCategoryDto>();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, Features.Categories.Commands.CreateCategoryCollection.CreateCategoryCommandDto>().ReverseMap();
        CreateMap<Category, Features.Categories.Commands.CreateCategoryCollection.CreateCategoryDto>();
        CreateMap<Category, CategoryCollectionVm>(); 
        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Category, CategoryForInsert>();

        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, CreateProductDto>();
        CreateMap<Product, ProductForInsert>();
        CreateMap<Product, UpdateProductCommand>().ReverseMap();
        CreateMap<Product, ProductForUpdateDto>().ReverseMap();
        CreateMap<Product, PartiallyUpdateProductCommand>().ReverseMap();
        CreateMap<Product, ProductExportDto>()
            .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Id));
        CreateMap<Features.Categories.Commands.CreateCategoryCollection.CreateCategoryCommandDto, CreateCategoryCommand>();


        CreateMap<Category, Features.Categories.Queries.Vanilla.GetCategoriesList.CategoryListVm>();
        CreateMap<Category, Features.Categories.Queries.Vanilla.GetCategoriesWithProducts.CategoryProductListVm>();
        CreateMap<Category, Features.Categories.Queries.Vanilla.GetCategoryDetail.CategoryDetailVm>();
        CreateMap<Category, Features.Categories.Commands.Vanilla.CreateCategory.CreateCategoryDto>();
        CreateMap<Category, Features.Categories.Commands.Vanilla.CreateCategory.CreateCategoryCommand>().ReverseMap();

        CreateMap<Customer, CustomerListDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge(src.DateOfDeath)));
        CreateMap<Customer, CustomerDetailVm>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge(src.DateOfDeath)));
        CreateMap<Customer, CreateCustomerDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge(src.DateOfDeath)));
        CreateMap<Customer, CreateCustomerWithDateOfDeathDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge(src.DateOfDeath)));
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
        CreateMap<Customer, CreateCustomerWithDateOfDeathCommand>().ReverseMap();
        CreateMap<Customer, CustomerFullDetailVm>();

        CreateMap<Image, ImageListDto>();
        CreateMap<Image, ImageDetailDto>();
        CreateMap<Image, CreateImageCommand>().ReverseMap();
        CreateMap<Image, CreateImageDto>();
        CreateMap<Image, UpdateImageCommand>().ReverseMap();
    }
}
