using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Wenlin.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "Your role(s)", new [] { "role" })
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
            {
                    new ApiResource("wenlincoreapi", "Wenlin Image Gallery API")
            };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { };

    public static IEnumerable<Client> Clients =>
        new Client[] 
        { 
            new Client()
            {
                ClientName= "Wenlin Core Image Gallery",
                ClientId= "wenlincoreclient",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris =
                {
                    "https://localhost:7012/signin-oidc"
                },                    
                PostLogoutRedirectUris =
                {
                    "https://localhost:7012/signout-callback-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    "wenlincoreapi"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RequireConsent= true

            }
        };
}