namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public interface IInfrastructureService : IService
    {
        IInfrastructure Infrastructure { get; }
    }
}