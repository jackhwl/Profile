using Microsoft.EntityFrameworkCore;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain;
using Image = Wenlin.Domain.Entities.Image;

namespace Wenlin.Persistence.Repositories;
public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public ImageRepository(WenlinContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Image>> ListAllAsync(Guid ownerId)
    {
        return await _dbContext.Set<Image>()
            .Where(i => i.OwnerId == ownerId)
            .OrderBy(i => i.Title).ToListAsync();
    }
    public async Task<bool> IsImageOwnerAsync(Guid id, Guid ownerId)
    {
        return await _dbContext.Set<Image>()
            .AnyAsync(i => i.Id == id && i.OwnerId == ownerId);
    }
}
