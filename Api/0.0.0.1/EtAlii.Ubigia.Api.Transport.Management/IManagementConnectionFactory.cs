namespace EtAlii.Ubigia.Api.Management
{
    public interface IManagementConnectionFactory
    {
        IManagementConnection Create(IManagementConnectionConfiguration configuration);
    }
}