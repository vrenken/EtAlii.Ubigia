namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Windows;

    public partial class QueryResultView
    {
        public QueryResultView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Visibility = DataContext is IGraphQueryLanguageViewModel 
                ? Visibility.Visible 
                : Visibility.Hidden;
            
            if (!Editor.IsInitialized)
            {
                Editor.BeginInit();
            }
        }
    }
}
