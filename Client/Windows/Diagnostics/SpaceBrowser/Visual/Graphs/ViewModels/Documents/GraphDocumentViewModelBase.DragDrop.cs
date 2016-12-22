namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System.Collections;
    using System.Windows;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Windows.Mvvm;
    using Northwoods.GoXam.Model;

    public partial class GraphDocumentViewModelBase : GraphLinksModel<EntryNode, Identifier, string, EntryLink>, IGraphDocumentViewModel, IDragDropHandler
    {
        public bool CanDrop(IDataObject dropObject, IEnumerable dropTarget)
        {
            var identifierType = typeof(Identifier);
            return dropObject.GetDataPresent(identifierType);
        }

        public void OnDrop(IDataObject dropObject, IEnumerable dropTarget)
        {
            var identifierType = typeof(Identifier);
            var identifier = (Identifier)dropObject.GetData(identifierType);

            _commandProcessor.Process(new RetrieveEntryCommand(identifier, ProcessReason.Retrieved));
        }
    }
}
