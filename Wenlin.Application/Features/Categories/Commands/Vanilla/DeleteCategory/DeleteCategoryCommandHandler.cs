using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Categories.Commands.Vanilla.DeleteCategory;
public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
{
    private readonly IAsyncRepository<Category> _categoryRepository;
    public DeleteCategoryCommandHandler(IAsyncRepository<Category> categoryRepository)
    {
        _categoryRepository= categoryRepository;
    }

    public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var deleteCategoryCommandResponse = new DeleteCategoryCommandResponse();

        var categoryToDelete = await _categoryRepository.GetByIdAsync(request.Id);
        if (categoryToDelete == null)
        {
            deleteCategoryCommandResponse.Success= false;
            deleteCategoryCommandResponse.NotFound = true;
        }
        else
        {
            await _categoryRepository.DeleteAsync(categoryToDelete);
        }

        return deleteCategoryCommandResponse;
    }
}
