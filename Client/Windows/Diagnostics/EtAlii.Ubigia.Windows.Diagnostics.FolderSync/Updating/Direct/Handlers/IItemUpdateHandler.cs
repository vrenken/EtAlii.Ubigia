namespace EtAlii.Ubigia.Diagnostics.FolderSync
{
    using System.ComponentModel;

    internal interface IItemUpdateHandler
    {
        void Handle(ItemCheckAction action, string localStart, string remoteStart);
    }
}
