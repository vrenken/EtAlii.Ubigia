namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for SettingsRibbonGroupBox.xaml
    /// </summary>
    public partial class Ribbon
    {
        public object LastFocusedDocument { get { return GetValue(LastFocusedDocumentProperty); } set { SetValue(LastFocusedDocumentProperty, value); } }
        public static readonly DependencyProperty LastFocusedDocumentProperty = DependencyProperty.Register("LastFocusedDocument", typeof(object), typeof(Ribbon), new PropertyMetadata(null, OnLastFocusedDocumentChanged));

        public Ribbon()
        {
            InitializeComponent();
            DataContext = null;
        }

        private static void OnLastFocusedDocumentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Update((Ribbon)sender, e.NewValue);
        }

        private void OnRibbonInitialized(object sender, EventArgs e)
        {
            Update((Ribbon)sender, null);
        }

        private static void Update(Ribbon ribbon, object newValue)
        {
            if (newValue is IGraphDocumentViewModel) // || e.NewValue is TreeView)
            {
                ribbon.SelectedTabItem = ribbon.GraphRibbonTabItem;
                ribbon.SelectedTabItem.DataContext = newValue;
            }
            else if (newValue is ICodeViewModel)
            {
                ribbon.SelectedTabItem = ribbon.CodeRibbonTabItem;
                ribbon.SelectedTabItem.DataContext = newValue;
            }
            else if (newValue is IGraphScriptLanguageViewModel)
            {
                ribbon.SelectedTabItem = ribbon.QueryRibbonTabItem;
                ribbon.SelectedTabItem.DataContext = newValue;
            }
            else if (newValue is IProfilingViewModel)
            {
                ribbon.SelectedTabItem = ribbon.ProfilingRibbonTabItem;
                ribbon.SelectedTabItem.DataContext = newValue;
            }
            else if (newValue == null)
            {
                var backstage = (Fluent.Backstage) ribbon.Menu;
                backstage.IsOpen = true;
            }
        }

    }
}
