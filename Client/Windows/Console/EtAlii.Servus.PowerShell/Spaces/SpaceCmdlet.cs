namespace EtAlii.Servus.PowerShell.Spaces
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.PowerShell.Storages;

    public class SpaceCmdlet : StorageTargetingCmdlet, IStorageInfoProvider
    {
        public static Space Current { get; set; }
    }
}
