namespace EtAlii.Ubigia.Infrastructure.Logical
{
    public interface ILogicalContextFactory
    {
        ILogicalContext Create(ILogicalContextConfiguration configuration);
    }
}