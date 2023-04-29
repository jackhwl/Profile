using Wenlin.Domain.Entities;

namespace Wenlin.Application.Contracts.Persistence;
public interface IImageRepository : IAsyncRepository<Image>
{
    Task<IEnumerable<Image>> ListAllAsync(Guid ownerId);
    Task<bool> IsImageOwnerAsync(Guid id, Guid ownerId);
}
