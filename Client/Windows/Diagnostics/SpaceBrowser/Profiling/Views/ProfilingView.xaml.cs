namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Syncfusion.Windows.Controls.Grid;

    public partial class ProfilingView //, IProfilingView
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
            //[
            //    gridTreeControl.Model.
            //}
            _expandAllAtTheEndSubscription = Observable
                .FromEventPattern<GridTreeCreatingNodeEventArgs>(GridTreeControl, "CreatingTreeNode")
                .Throttle(TimeSpan.FromSeconds(2))
                .ObserveOnDispatcher()
                .Subscribe(e =>
                {
                    if (AutoExpandNodes)
                    {
                        try
                        {
                            GridTreeControl.Dispatcher.Invoke(GridTreeControl.ExpandAllNodes);
                        }
                        catch (Exception)
                        {
                            //throw
                        }
                    }
                });

            _expandAlwaysSubscription = Observable
                .FromEventPattern<GridTreeCreatingNodeEventArgs>(GridTreeControl, "CreatingTreeNode")
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
            var selectedNode = GridTreeControl.SelectedNode as GridTreeNode; 
            if (selectedNode != null)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        GridTreeControl.CollapseNode(selectedNode);
                        break;
                    case Key.Right:
                        GridTreeControl.ExpandNode(selectedNode);
                        break;
                }
            }
        }
    }
}   
