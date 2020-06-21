namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using EtAlii.Ubigia.Api;

    public static class StorageCmdlet 
    {
        public static Uri CurrentStorageApiAddress { get; set; }
        public static Storage CurrentStorage { get; set; }
    }
}
