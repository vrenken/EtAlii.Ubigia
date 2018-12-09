namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections;
    using System.Windows;
    using Syncfusion.UI.Xaml.Grid;

    public partial class QueryResultView
    {
        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(QueryResultView), new PropertyMetadata(null, OnSourceChanged));
 
        public QueryResultView()
        {
            InitializeComponent();
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IGraphScriptLanguageViewModel)
            {
                var srv = (QueryResultView)d;
                srv.DataContext = e.NewValue;
                
            }
        }
    }
}
