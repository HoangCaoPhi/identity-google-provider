using google_login.Server.Abtractions;
using google_login.Server.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace google_login.Server;

public static class DependencyInjections
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection(JwtOptions.SectionName)
                                     .Get<JwtOptions>();

        var key = Encoding.ASCII.GetBytes(jwtConfig.Key);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddHttpClient(GoogleLoginProvider.GoogleApiClientName, client =>
        {
            client.BaseAddress = new Uri("https://www.googleapis.com");
        });

        services.AddHttpClient(GoogleLoginProvider.OAuthApiClientName, client =>
        {
            client.BaseAddress = new Uri("https://oauth2.googleapis.com");
        });

        services.AddHttpClient<IReCaptchaService, GoogleReCaptchaService>((serviceProvider, client) =>
        {
            client.BaseAddress = new Uri("https://www.google.com/recaptcha/");
        });

        services.AddScoped<IGoogleLoginProvider, GoogleLoginProvider>();
        services.AddScoped<IReCaptchaService, GoogleReCaptchaService>();

        return services;
    }
}
