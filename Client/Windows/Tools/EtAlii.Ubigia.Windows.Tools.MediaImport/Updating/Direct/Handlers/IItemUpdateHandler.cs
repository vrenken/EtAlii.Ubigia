namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.ComponentModel;

    internal interface IItemUpdateHandler
    {
        void Handle(ItemCheckAction action, string localStart, string remoteStart);
    }
}
