namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Reactive.Linq;

    public static class IObservableToObservableCollectionExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IObservable<T> observable, MainDispatcherInvoker dispatcherInvoker)
        {
            var result = new ObservableCollection<T>();

            observable
                .Subscribe(o =>
            { 
                //try
                //{
                    dispatcherInvoker.Invoke(() =>
                    {
                        result.Add(o);
                    });
                //}
                //catch (Exception e)
                //{
                //}
            });

            return result;
        }
    }
}
