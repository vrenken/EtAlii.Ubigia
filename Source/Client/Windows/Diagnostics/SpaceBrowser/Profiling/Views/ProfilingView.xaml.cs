namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Syncfusion.Windows.Controls.Grid;

    public partial class ProfilingView //, IProfilingView
    {
        // ReSharper disable InconsistentNaming
        public bool AutoExpandNodes { get => (bool)GetValue(AutoExpandNodesProperty); set => SetValue(AutoExpandNodesProperty, value); }
        public static readonly DependencyProperty AutoExpandNodesProperty = DependencyProperty.Register("AutoExpandNodes", typeof(bool), typeof(ProfilingView), new PropertyMetadata(false));
        // ReSharper restore InconsistentNaming

        private readonly IDisposable _expandAllAtTheEndSubscription;
        private readonly IDisposable _expandAlwaysSubscription;
        public ProfilingView()
        {
            InitializeComponent();

            Unloaded += OnUnload;

            //gridTreeControl.ModelLoaded += (sender, args) =>
            //[
            //    gridTreeControl.Model.
            //]
            _expandAllAtTheEndSubscription = Observable
                .FromEventPattern<GridTreeCreatingNodeEventArgs>(GridTreeControl, "CreatingTreeNode")
                .Throttle(TimeSpan.FromSeconds(2))
                .ObserveOnDispatcher()
                .Subscribe(_ =>
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

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            _expandAlwaysSubscription?.Dispose();
            _expandAllAtTheEndSubscription?.Dispose();
        }

        private void GridTreeControl_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (GridTreeControl.SelectedNode is GridTreeNode selectedNode)
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
