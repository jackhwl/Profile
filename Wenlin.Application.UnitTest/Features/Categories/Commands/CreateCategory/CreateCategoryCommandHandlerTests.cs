using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shouldly;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Categories.Commands.CreateCategory;
using Wenlin.Application.Profiles;
using Wenlin.Domain;
using Wenlin.Domain.Entities;
using Wenlin.Persistence.Configurations;
using Wenlin.Persistence.Repositories;
using Wenlin.SharedKernel.Configuration;
using Xunit;

namespace Wenlin.Application.UnitTest.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandHandlerTests
{
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });
        _mapper = mapperConfig.CreateMapper();

        var options = new DbContextOptionsBuilder<WenlinContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new WenlinContext(options, new SqlModelConfiguration());
        _categoryRepository = new CategoryRepository(context);
    }

    [Fact]
    public async Task CanCreateACategory()
    {
        var handler = new CreateCategoryCommandHandler(_mapper, _categoryRepository);

        var result = await handler.Handle(new CreateCategoryCommand() { Name = "category 123" }, CancellationToken.None);

        result.Category.Name.ShouldBe("category 123");

        result = await handler.Handle(new CreateCategoryCommand() { Name = "category 456" }, CancellationToken.None);

        result.Category.Name.ShouldBe("category 456");

        var total = await _categoryRepository.ListAllAsync();
        total.Count.ShouldBe(2);
    }
}
