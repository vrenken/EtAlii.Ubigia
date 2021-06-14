namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public interface IInfrastructureTestHostBase : ITestHost
    {
        IInfrastructure Infrastructure { get; }
    }
}
