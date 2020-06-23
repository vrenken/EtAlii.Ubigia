namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;
    using EtAlii.Ubigia.Api;

    public static class StorageCmdlet 
    {
        public static Uri CurrentManagementApiAddress { get; set; }
        public static Uri CurrentDataApiAddress { get; set; }
        
        public static Storage CurrentStorage { get; set; }
    }
}
