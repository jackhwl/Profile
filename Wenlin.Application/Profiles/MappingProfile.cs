﻿using AutoMapper;
using Wenlin.Application.Features.Categories.Commands.CreateCategory;
using Wenlin.Application.Features.Categories.Commands.UpdateCategory;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesList;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesWithProducts;
using Wenlin.Application.Features.Categories.Queries.GetCategoryCollection;
using Wenlin.Application.Features.Categories.Queries.GetCategoryDetail;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Application.Features.Products.Commands.CreateProduct;
using Wenlin.Application.Features.Products.Commands.PartiallyUpdateProduct;
using Wenlin.Application.Features.Products.Commands.UpdateProduct;
using Wenlin.Application.Features.Products.Queries.GetProductDetail;
using Wenlin.Application.Features.Products.Queries.GetProductsExport;
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

        CreateMap<Customer, CustomerListDto>().ReverseMap();

    }
}
