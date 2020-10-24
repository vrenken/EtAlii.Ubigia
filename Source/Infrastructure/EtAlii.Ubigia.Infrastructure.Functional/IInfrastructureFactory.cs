namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public interface IInfrastructureFactory
    {
        IInfrastructure Create(InfrastructureConfiguration configuration);
    }
}
