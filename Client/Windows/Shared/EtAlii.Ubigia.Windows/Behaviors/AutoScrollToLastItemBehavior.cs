namespace EtAlii.Ubigia.Windows
{
    using System.Collections.Specialized;
    using System.Windows.Controls;
    using Microsoft.Xaml.Behaviors;

    public sealed class AutoScrollToLastItemBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject.Items.SourceCollection is INotifyCollectionChanged collection)
            {
                collection.CollectionChanged += OnCollectionChanged;
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject.Items.SourceCollection is INotifyCollectionChanged collection)
            {
                collection.CollectionChanged -= OnCollectionChanged;
            }

            base.OnDetaching();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ScrollToLastItem();
            }
        }

        private void ScrollToLastItem()
        {
            var count = AssociatedObject.Items.Count;
            if (count > 0)
            {
                var last = AssociatedObject.Items[count - 1];
                AssociatedObject.ScrollIntoView(last);
            }
        }
    }
}
