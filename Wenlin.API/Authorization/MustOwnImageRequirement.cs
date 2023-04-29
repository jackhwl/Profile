using Microsoft.AspNetCore.Authorization;

namespace Wenlin.API.Authorization;
public class MustOwnImageRequirement : IAuthorizationRequirement
{
    public MustOwnImageRequirement()
    {

    }
}
