namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections;
    using System.Windows;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Windows.Mvvm;
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

            OnDrop(identifier);
        }

        protected virtual void OnDrop(Identifier identifier)
        {
        }
    }
}
