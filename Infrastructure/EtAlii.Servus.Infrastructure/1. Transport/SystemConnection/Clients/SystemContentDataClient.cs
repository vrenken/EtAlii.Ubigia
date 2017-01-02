namespace EtAlii.Servus.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Functional;

    internal partial class SystemContentDataClient : SystemSpaceClientBase, IContentDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemContentDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }
    }
}
