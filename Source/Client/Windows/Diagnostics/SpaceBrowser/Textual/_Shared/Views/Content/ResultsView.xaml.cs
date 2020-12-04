namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections;
    using System.Windows;

    public partial class ResultsView
    {
        public IEnumerable ItemsSource { get => (IEnumerable)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value ?? Array.Empty<object>()); }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ResultsView), new PropertyMetadata(Array.Empty<object>(), OnItemsSourceChanged));

        public object SelectedItem { get => GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ResultsView), new PropertyMetadata(null));

        public object Source { get => GetValue(SourceProperty); set => SetValue(SourceProperty, value); }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(ResultsView), new PropertyMetadata(null, OnSourceChanged));

        public ResultsView()
        {
            InitializeComponent();
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resultView = (ResultsView) d;
            resultView.SelectedItem = null;
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resultsView = (ResultsView) d;
            var scriptViewModel = e.NewValue as IGraphScriptLanguageViewModel;
            resultsView.Visibility = scriptViewModel != null ? Visibility.Visible : Visibility.Hidden;
            resultsView.DataContext = scriptViewModel != null ? e.NewValue : null;
        }
    }
}
