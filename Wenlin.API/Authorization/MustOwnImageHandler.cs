using Microsoft.AspNetCore.Authorization;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.API.Authorization;
public class MustOwnImageHandler : AuthorizationHandler<MustOwnImageRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageRepository _imageRepository;
    public MustOwnImageHandler(IHttpContextAccessor httpContextAccessor, IImageRepository imageRepository)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnImageRequirement requirement)
    {
        var imageId = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

        if (!Guid.TryParse(imageId, out Guid imageIdAsGuid))
        {
            context.Fail();
            return;
        }

        // get the sub claim
        var ownerId = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        // if it cannot be found, the handler fails
        if (ownerId == null)
        {
            context.Fail();
            return;
        }

        if (!await _imageRepository.IsImageOwnerAsync(imageIdAsGuid, Guid.Parse(ownerId)))
        {
            context.Fail();
            return;
        }

        // all checks out
        context.Succeed(requirement);
    }
}
