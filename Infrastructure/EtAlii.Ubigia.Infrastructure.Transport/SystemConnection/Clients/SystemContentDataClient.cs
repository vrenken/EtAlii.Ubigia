namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal partial class SystemContentDataClient : SystemSpaceClientBase, IContentDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemContentDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }
    }
}
