using Moq;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.UnitTest.Mocks;
public static class MockCategoryRepository
{
    public static Mock<ICategoryRepository> GetCategoryRepository()
    {
        var categories = new List<Category>
        {
            new Category
            {
                Id = new Guid("7A540B5A-76C7-4781-9A6A-2FABFEE6EB7D"),
                Name = "Category A",
                Description = "Category A Description",
            },
            new Category
            {
                Id = new Guid("BD1E0DF3-1737-4676-8E7D-2D038181655C"),
                Name = "Category B"
            }
        };

        var mockRepo = new Mock<ICategoryRepository>();

        mockRepo.Setup(r => r.ListAllAsync()).ReturnsAsync(categories);
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => categories.FirstOrDefault(c => c.Id == id));

        return mockRepo;
    }
}
