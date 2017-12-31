namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System.Linq;
    using System.Text;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public class AuthenticationService : AspNetCoreServiceBase, IAuthenticationService
    {
        private readonly IConfigurationSection _configuration;

        public AuthenticationService(IConfigurationSection configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure;

            // Source: https://auth0.com/blog/securing-asp-dot-net-core-2-applications-with-jwts/
            applicationBuilder.UseBranchWithServices(Port, "/authenticate",
                services =>
                {
                    services
                        .AddSingleton<IConfigurationSection>(_configuration)
                        .AddSingleton<IAccountRepository>(infrastructure.Accounts)
                        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = _configuration["Jwt:Issuer"],
                                ValidAudience = _configuration["Jwt:Issuer"],
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                            };
                        });
                    services
                        .AddMvcForTypedController<AuthenticateController>();
                },
                appBuilder =>
                {
                    appBuilder
                        .UseAuthentication()
                        .UseMvc();
                });
        }
    }
}
