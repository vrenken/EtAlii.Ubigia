namespace EtAlii.Ubigia.Windows.Mvvm
{
    using System.Collections;
    using System.Windows;

    public interface IDragDropHandler
    {
        bool CanDrop(IDataObject dropObject, IEnumerable dropTarget);

        void OnDrop(IDataObject dropObject, IEnumerable dropTarget);
    }

}
