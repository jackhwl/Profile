using AutoMapper;
using Moq;
using Shouldly;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Categories.Queries.GetCategoryDetail;
using Wenlin.Application.Profiles;
using Wenlin.Application.UnitTest.Mocks;
using Xunit;

namespace Wenlin.Application.UnitTest.Features.Categories.Queries.GetCategoryDetail;
public class GetCategoryDetailQueryHandlerTests
{
	private readonly IMapper _mapper;
	private readonly Mock<ICategoryRepository> _mockRepo;
	public GetCategoryDetailQueryHandlerTests()
	{
		_mockRepo = MockCategoryRepository.GetCategoryRepository();

		var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });

		_mapper = mapperConfig.CreateMapper();
	}

	[Fact]
	public async Task GetCategoryDetailQueryTest()
	{
		var handler = new GetCategoryDetailQueryHandler(_mockRepo.Object, _mapper);

		var result = await handler.Handle(new GetCategoryDetailQuery() { Id = new Guid("7A540B5A-76C7-4781-9A6A-2FABFEE6EB7D") }, CancellationToken.None);

		result.ShouldBeOfType<CategoryDetailVm>();
		result.Id.ShouldBe(new Guid("7A540B5A-76C7-4781-9A6A-2FABFEE6EB7D"));
		result.Name.ShouldBe("Category A");
	}
}
