namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for SettingsRibbonGroupBox.xaml
    /// </summary>
    public partial class ApplicationRibbon
    {
        public object LastFocusedDocument { get => GetValue(LastFocusedDocumentProperty); set => SetValue(LastFocusedDocumentProperty, value); }
        public static readonly DependencyProperty LastFocusedDocumentProperty = DependencyProperty.Register("LastFocusedDocument", typeof(object), typeof(ApplicationRibbon), new PropertyMetadata(null, OnLastFocusedDocumentChanged));

        public ApplicationRibbon()
        {
            InitializeComponent();
            DataContext = null;
        }

        private static void OnLastFocusedDocumentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Update((ApplicationRibbon)sender, e.NewValue);
        }

        private void OnRibbonInitialized(object sender, EventArgs e)
        {
            Update((ApplicationRibbon)sender, null);
        }

        private static void Update(ApplicationRibbon applicationRibbon, object newValue)
        {
            switch (newValue)
            {
                case IGraphDocumentViewModel _:
                    applicationRibbon.SelectedTabItem = applicationRibbon.GraphRibbonTabItem;
                    applicationRibbon.SelectedTabItem.DataContext = newValue;
                    break;
                case ICodeViewModel _:
                    applicationRibbon.SelectedTabItem = applicationRibbon.CodeRibbonTabItem;
                    applicationRibbon.SelectedTabItem.DataContext = newValue;
                    break;
                case IGraphScriptLanguageViewModel _:
                    applicationRibbon.SelectedTabItem = applicationRibbon.ScriptRibbonTabItem;
                    applicationRibbon.SelectedTabItem.DataContext = newValue;
                    break;
                case IGraphQueryLanguageViewModel _:
                    applicationRibbon.SelectedTabItem = applicationRibbon.QueryRibbonTabItem;
                    applicationRibbon.SelectedTabItem.DataContext = newValue;
                    break;
                case IProfilingViewModel _:
                    applicationRibbon.SelectedTabItem = applicationRibbon.ProfilingRibbonTabItem;
                    applicationRibbon.SelectedTabItem.DataContext = newValue;
                    break;
                default:
                    if (newValue == null)
                    {
                        var backstage = (Fluent.Backstage) applicationRibbon.Menu;
                        backstage.IsOpen = true;
                    }
                    break;
            }
        }
    }
}
