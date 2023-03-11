using AutoMapper;
using Wenlin.API.Models;
using Wenlin.Domain.Entities;

namespace Wenlin.API.Profiles;

public class CategoryProfile : Profile
{
	public CategoryProfile()
	{
		CreateMap<Category, CategoryDto>();
		CreateMap<CategoryForCreationDto, Category>();
	}
}

