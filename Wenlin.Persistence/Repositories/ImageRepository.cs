using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain;
using Wenlin.Domain.Entities;

namespace Wenlin.Persistence.Repositories;
public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public ImageRepository(WenlinContext dbContext) : base(dbContext) { }
}
