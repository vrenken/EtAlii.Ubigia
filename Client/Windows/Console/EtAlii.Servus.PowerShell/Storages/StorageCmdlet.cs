namespace EtAlii.Servus.PowerShell.Storages
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    public class StorageCmdlet : StorageCmdletBase
    {
        public static Storage Current { get; set; }
    }
}
