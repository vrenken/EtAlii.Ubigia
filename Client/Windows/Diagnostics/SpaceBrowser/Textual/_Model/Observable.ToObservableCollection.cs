namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.ObjectModel;

    public static class ObservableToObservableCollectionExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IObservable<T> observable, IMainDispatcherInvoker dispatcherInvoker)
        {
            var result = new ObservableCollection<T>();

            observable
                .Subscribe(o =>
            { 
                dispatcherInvoker.Invoke(() =>
                {
                    result.Add(o);
                });
            });

            return result;
        }
    }
}
