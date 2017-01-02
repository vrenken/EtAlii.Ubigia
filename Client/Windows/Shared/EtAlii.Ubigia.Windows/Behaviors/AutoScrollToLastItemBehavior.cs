namespace EtAlii.Ubigia.Windows
{
    using System.Collections.Specialized;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    public sealed class AutoScrollToLastItemBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            var collection = AssociatedObject.Items.SourceCollection as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged += OnCollectionChanged;
            }
        }

        protected override void OnDetaching()
        {
            var collection = AssociatedObject.Items.SourceCollection as INotifyCollectionChanged;
            if (collection != null)
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
            int count = AssociatedObject.Items.Count;
            if (count > 0)
            {
                var last = AssociatedObject.Items[count - 1];
                AssociatedObject.ScrollIntoView(last);
            }
        }
    }
}
