namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.PowerShell.Storages;

    public class SpaceCmdlet : StorageTargetingCmdlet, IStorageInfoProvider
    {
        public static Space Current { get; set; }
    }
}
