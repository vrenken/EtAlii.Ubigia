namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public interface IObservableFolderSyncConfigurationCollection : ICollection<FolderSyncConfiguration>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        
    }
}