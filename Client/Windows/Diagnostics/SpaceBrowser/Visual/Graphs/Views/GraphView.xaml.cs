﻿namespace EtAlii.Servus.Windows.Diagnostics.SpaceBrowser.Views
{
    public partial class GraphView : GraphViewBase
    {
        public GraphView()
        {
            InitializeComponent();
        }

        private void OnContextMenuOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            //((ContextMenu)sender).DataContext = this.DataContext;
        }
    }
}
