namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections;
    using System.Windows;
    using Syncfusion.UI.Xaml.Grid;

    public partial class ScriptResultView
    {
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value ?? new object[0]); }
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ScriptResultView), new PropertyMetadata(null, OnItemsSourceChanged));

        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(ScriptResultView), new PropertyMetadata(null, OnSourceChanged));

        public GridVirtualizingCollectionView ItemsSourceView 
        {
            get { return (GridVirtualizingCollectionView)GetValue(ItemsSourceViewProperty); }
            set { SetValue(ItemsSourceViewProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceViewProperty = DependencyProperty.Register("ItemsSourceView", typeof(GridVirtualizingCollectionView), typeof(ScriptResultView), null);
 
        public ScriptResultView()
        {
            InitializeComponent();
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IScriptViewModel)
            {
                var srv = (ScriptResultView)d;
                srv.DataContext = e.NewValue;
                if (!srv.DataGrid.IsInitialized)
                {
                    srv.DataGrid.BeginInit();
                }
            }
        }
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = new GridVirtualizingCollectionView(e.NewValue as IEnumerable);
            var srv = (ScriptResultView)d;
            srv.ItemsSourceView = view;
        }
    }
}
