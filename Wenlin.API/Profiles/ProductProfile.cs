using AutoMapper;
using Wenlin.API.Models;
using Wenlin.Domain.Entities;

namespace Wenlin.API.Profiles;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>();
		CreateMap<ProductForCreationDto, Product>();
	}
}

