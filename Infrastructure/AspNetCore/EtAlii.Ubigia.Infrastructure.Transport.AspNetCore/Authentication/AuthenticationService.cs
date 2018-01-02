namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
                        .AddSingleton<IAuthenticationVerifier, AuthenticationVerifier>()
                        .AddSingleton<IAuthenticationTokenVerifier, AuthenticationTokenVerifier>()
                        .AddSingleton<IAuthenticationIdentityProvider, DefaultAuthenticationIdentityProvider>()
                        .AddSingleton<IAccountRepository>(infrastructure.Accounts);
                        //.AddAuthentication(options =>
                        //{
                        //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        //})
                        //.AddJwtBearer(options =>
                        //{
                        //    options.TokenValidationParameters = new TokenValidationParameters
                        //    {
                        //        ValidateIssuer = true,
                        //        ValidateAudience = true,
                        //        ValidateIssuerSigningKey = true,
                        //        ValidIssuer = _configuration["Jwt:Issuer"],
                        //        ValidAudience = _configuration["Jwt:Issuer"],
                        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                        //        ValidateLifetime = true, //validate the expiration and not before values in the token
                        //        ClockSkew = TimeSpan.FromMinutes(1) //5 minute tolerance for the expiration date

                        //    };
                        //});
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
