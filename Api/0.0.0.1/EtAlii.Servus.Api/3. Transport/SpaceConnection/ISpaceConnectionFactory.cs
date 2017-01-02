namespace EtAlii.Servus.Api.Transport
{
    public interface ISpaceConnectionFactory
    {
        ISpaceConnection Create(ISpaceConnectionConfiguration configuration);
    }
}