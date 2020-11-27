namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections;
    using System.Windows;
    using EtAlii.Ubigia.Windows.Mvvm;

    public partial class GraphDocumentViewModelBase : IDragDropHandler
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

        protected virtual void OnDrop(in Identifier identifier)
        {
            // Handle a drop of an identifier.
        }
    }
}
