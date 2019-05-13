namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Threading.Tasks;

    internal interface IItemUpdateHandler
    {
        Task Handle(ItemCheckAction action, string localStart, string remoteStart);
    }
}
