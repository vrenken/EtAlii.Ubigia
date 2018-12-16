namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections;
    using System.Windows;

    public partial class ErrorsView
    {
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value ?? new object[0]);
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ErrorsView), new PropertyMetadata(new object[0]));

        public object Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(ErrorsView), new PropertyMetadata(null, OnSourceChanged));

        public ErrorsView()
        {
            InitializeComponent();
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case IGraphScriptLanguageViewModel scriptLanguageViewModel:
                    ((ErrorsView)d).DataContext = scriptLanguageViewModel;
                    break;
                case IGraphQueryLanguageViewModel queryLanguageViewModel:
                    ((ErrorsView)d).DataContext = queryLanguageViewModel;
                    break;
            }
        }
    }
}
