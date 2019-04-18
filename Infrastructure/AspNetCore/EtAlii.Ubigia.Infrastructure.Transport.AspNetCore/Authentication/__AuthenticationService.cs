//namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
//{
//    using System.Linq
//    using System.Net
//    using EtAlii.xTechnology.Hosting
//    using Microsoft.AspNetCore.Builder
//    using Microsoft.Extensions.Configuration
//    using Microsoft.Extensions.DependencyInjection

//	public class AuthenticationService : AspNetCoreServiceBase, IAuthenticationService
//    {
//        private readonly IConfigurationSection _configuration

//        public AuthenticationService(IConfigurationSection configuration) : base(configuration)
//        {
//            _configuration = configuration
//        }

//        protected override void OnConfigureApplication(IApplicationBuilder applicationBuilder)
//        {
//            var infrastructure = System.Services.OfType<IInfrastructureService>().Single().Infrastructure

//            // Source: https://auth0.com/blog/securing-asp-dot-net-core-2-applications-with-jwts/
//            applicationBuilder.UseBranchWithServices(Port, "/authenticate",
//                services =>
//                {
//	                services
//		                //.AddAuthorization(options => options.AddPolicy("AuthorizedAdministrator", policy => policy.Requirements.Add(new AuthorizationAdministratorRequirement())))
//		                .AddInfrastructureHttpContextAuthentication(infrastructure)
//                        .AddMvcForTypedController<AuthenticateController>()
//                },
//                appBuilder =>
//                {
//                    appBuilder
//                        .UseAuthentication()
//                        .UseMvc()
//                })
//        }
//    }
//}
