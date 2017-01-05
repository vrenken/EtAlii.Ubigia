namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    internal class AdminApiInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IInfrastructureConfiguration _configuration;

        public AdminApiInfrastructureExtension(IInfrastructureConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(Container container)
        {
        }
    }
}