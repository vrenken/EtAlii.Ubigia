namespace EtAlii.Servus.Windows.Diagnostics.SpaceBrowser.Views
{
    using System;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Syncfusion.Windows.Controls.Grid;

    public partial class ProfilingView : UserControl
    {
        public bool AutoExpandNodes
        {
            get { return (bool)GetValue(AutoExpandNodesProperty); }
            set { SetValue(AutoExpandNodesProperty, value); }
        }
        public static readonly DependencyProperty AutoExpandNodesProperty = DependencyProperty.Register("AutoExpandNodes", typeof(bool), typeof(ProfilingView), new PropertyMetadata(false));

        private readonly IDisposable _expandAllAtTheEndSubscription;
        private readonly IDisposable _expandAlwaysSubscription;
        public ProfilingView()
        {
            InitializeComponent();

            //gridTreeControl.ModelLoaded += (sender, args) =>
            //{
            //    gridTreeControl.Model.
            //}
            _expandAllAtTheEndSubscription = Observable
                .FromEventPattern<GridTreeCreatingNodeEventArgs>(this.gridTreeControl, "CreatingTreeNode")
                .Throttle(TimeSpan.FromSeconds(2))
                .ObserveOnDispatcher()
                .Subscribe(e =>
                {
                    if (AutoExpandNodes)
                    {
                        try
                        {
                            gridTreeControl.Dispatcher.Invoke(gridTreeControl.ExpandAllNodes);
                        }
                        catch (Exception)
                        {
                            //throw;
                        }
                    }
                });

            _expandAlwaysSubscription = Observable
                .FromEventPattern<GridTreeCreatingNodeEventArgs>(this.gridTreeControl, "CreatingTreeNode")
                .ObserveOnDispatcher()
                .Subscribe(e =>
                {
                    if (e.EventArgs.Level > 0 && AutoExpandNodes)
                    {
                        e.EventArgs.ParentNode.Expanded = true;
                    }
                });
        }

        private void GridTreeControl_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            var selectedNode = gridTreeControl.SelectedNode as GridTreeNode; 
            if (selectedNode != null)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        gridTreeControl.CollapseNode(selectedNode);
                        break;
                    case Key.Right:
                        gridTreeControl.ExpandNode(selectedNode);
                        break;
                }
            }
        }
    }
}   
