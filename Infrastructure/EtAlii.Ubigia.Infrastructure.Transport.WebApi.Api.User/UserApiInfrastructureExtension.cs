namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    internal class UserApiInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IInfrastructureConfiguration _configuration;

        public UserApiInfrastructureExtension(IInfrastructureConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(Container container)
        {
        }
    }
}