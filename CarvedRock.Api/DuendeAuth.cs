using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CarvedRock.Api
{
    public static class DuendeAuth
    {
        public static IServiceCollection AddDemoDuendeJwtBearer(this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://demo.duendesoftware.com";
                    options.Audience = "api";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "email"
                    };
                });
            return services;
        }
    }
}
