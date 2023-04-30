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
            new IdentityResource("roles", "Your role(s)", new [] { "role" }),
            new IdentityResource("country", "The country you're living in", new List<string>() { "country" })

        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
            {
                new ApiResource("wenlincoreapi", "Wenlin Image Gallery API", new [] { "role", "country"  })
                {
                    Scopes = { "wenlincoreapi.fullaccess", "wenlincoreapi.read", "wenlincoreapi.write" }
                }
            };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("wenlincoreapi.fullaccess"),
            new ApiScope("wenlincoreapi.read"),
            new ApiScope("wenlincoreapi.write")
        };

    public static IEnumerable<Client> Clients =>
        new Client[] 
        { 
            new Client()
            {
                ClientName= "Wenlin Core Image Gallery",
                ClientId= "wenlincoreclient",
                AllowedGrantTypes = GrantTypes.Code,
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                AccessTokenLifetime = 120, // 2 min
                // AuthorizationCodeLifetime = ...
                // IdentityTokenLifetime = ...
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
                    //"wenlincoreapi.fullaccess",
                    "wenlincoreapi.read",
                    "wenlincoreapi.write",
                    "country"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RequireConsent= true

            }
        };
}