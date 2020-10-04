namespace EtAlii.Ubigia.Api.Transport.Management
{
    public interface IManagementConnectionFactory
    {
        IManagementConnection Create(IManagementConnectionConfiguration configuration);
    }
}