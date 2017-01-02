namespace EtAlii.Ubigia.Diagnostics.FolderSync
{
    public struct ItemCheckAction
    {
        public ItemChange Change { get; set; }
        public string OldItem { get; set; }
        public string Item { get; set; }
    }
}
