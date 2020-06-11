namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public interface IInfrastructureTestHostBase : IHost
    {
        IInfrastructure Infrastructure { get; }
    }
}