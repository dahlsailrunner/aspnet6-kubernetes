using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CarvedRock.UI;

public static class DuendeAuth
{
    public static IServiceCollection AddDemoDuendeIdentityServer(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://demo.duendesoftware.com";
                options.ClientId = "interactive.confidential";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("api");
                options.Scope.Add("offline_access");
                options.GetClaimsFromUserInfoEndpoint = true;

                options.SaveTokens = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "email"
                };
            });

        services.AddAccessTokenManagement();
        return services;

        
    }
}

