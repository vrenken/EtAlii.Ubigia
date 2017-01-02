namespace EtAlii.Servus.Api.Tests
{
    using EtAlii.Servus.Api.Transport.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;

    public interface ITestHostingStorage
    {
        string SpaceName { get; set; }
        void Start(ITransportTestContext transportTestContext, ITestHosting testHosting);
        void Stop();
    }
}