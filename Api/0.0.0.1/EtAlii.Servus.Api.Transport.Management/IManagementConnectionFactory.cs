namespace EtAlii.Servus.Api.Management
{
    public interface IManagementConnectionFactory
    {
        IManagementConnection Create(IManagementConnectionConfiguration configuration);
    }
}