namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections;
    using System.Windows;
    using Syncfusion.UI.Xaml.Grid;

    public partial class ScriptResultView
    {
        public IEnumerable ItemsSource { get => (IEnumerable)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value ?? Array.Empty<object>()); }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ScriptResultView), new PropertyMetadata(null, OnItemsSourceChanged));

        public GridVirtualizingCollectionView ItemsSourceView { get => (GridVirtualizingCollectionView)GetValue(ItemsSourceViewProperty); set => SetValue(ItemsSourceViewProperty, value); }
        public static readonly DependencyProperty ItemsSourceViewProperty = DependencyProperty.Register("ItemsSourceView", typeof(GridVirtualizingCollectionView), typeof(ScriptResultView), null);

        public ScriptResultView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Visibility = DataContext is IGraphScriptLanguageViewModel 
                ? Visibility.Visible 
                : Visibility.Hidden;
            
            if (!DataGrid.IsInitialized)
            {
                DataGrid.BeginInit();
            }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scriptResultView = (ScriptResultView)d;

            if (e.NewValue is IEnumerable enumerable)
            {
                var view = new GridVirtualizingCollectionView(enumerable);
                scriptResultView.ItemsSourceView = view;    
            }
            else
            {
                scriptResultView.ItemsSourceView = null;
            }
            
        }
    }
}
