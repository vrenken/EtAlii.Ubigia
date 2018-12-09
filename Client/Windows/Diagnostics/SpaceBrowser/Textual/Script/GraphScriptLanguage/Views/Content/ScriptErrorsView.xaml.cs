namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections;
    using System.Windows;

    public partial class ScriptErrorsView
    {
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value ?? new object[0]); }
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ScriptErrorsView), new PropertyMetadata(new object[0]));

        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(ScriptErrorsView), new PropertyMetadata(null, OnSourceChanged));

        public ScriptErrorsView()
        {
            InitializeComponent();
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IGraphScriptLanguageViewModel)
            {
                ((ScriptErrorsView)d).DataContext = e.NewValue;
            }
        }
    }
}
