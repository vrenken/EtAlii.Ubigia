namespace EtAlii.Servus.Infrastructure.Functional
{
    public interface IInfrastructureFactory
    {
        IInfrastructure Create(IInfrastructureConfiguration configuration);
    }
}
