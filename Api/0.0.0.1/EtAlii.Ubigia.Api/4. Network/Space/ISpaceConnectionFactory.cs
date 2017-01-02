namespace EtAlii.Ubigia.Api.Transport
{
    public interface ISpaceConnectionFactory
    {
        ISpaceConnection Create(ISpaceConnectionConfiguration configuration);
    }
}