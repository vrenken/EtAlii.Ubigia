namespace EtAlii.Ubigia.PowerShell.Storages
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public class StorageCmdlet : StorageCmdletBase
    {
        public static Storage Current { get; set; }
    }
}
