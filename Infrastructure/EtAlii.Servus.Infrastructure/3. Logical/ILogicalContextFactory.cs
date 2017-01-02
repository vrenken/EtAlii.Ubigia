namespace EtAlii.Servus.Infrastructure.Logical
{
    public interface ILogicalContextFactory
    {
        ILogicalContext Create(ILogicalContextConfiguration configuration);
    }
}