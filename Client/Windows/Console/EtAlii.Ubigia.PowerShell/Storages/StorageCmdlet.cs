namespace EtAlii.Ubigia.PowerShell.Storages
{
    using System;

    public static class StorageCmdlet 
    {
        public static Uri CurrentManagementApiAddress { get; set; }
        public static Uri CurrentDataApiAddress { get; set; }
        
        public static Storage CurrentStorage { get; set; }
    }
}
