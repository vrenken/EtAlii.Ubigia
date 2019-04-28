namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Linq;
    using Northwoods.GoXam;
    using Northwoods.GoXam.Layout;

    // implement various optimizations
    public class ContinuousLayoutManager : LayoutManager
    {
        // optimize the creation of the ForceDirectedNetwork:
        // only create it when there are Nodes or Links added or removed
        protected override void PerformLayout()
        {
            var diagram = Diagram;
            if (diagram == null) return;
            if (diagram.Layout is ContinuousForceDirectedLayout layout)
            {
                // assume there are no Groups with their own Layout
                if (layout.ValidLayout) return;
                
                // make sure there's no animation interfering with ContinuousForceDirectedLayout
                Animated = false;
                // don't set Wait cursor during drag
                diagram.Cursor = null;
                // the ForceDirectedNetwork needs to be cleared whenever a Node or Link is added or removed
                var net = layout.Network;
                if (net == null)
                {  // create a new one
                    net = new ForceDirectedNetwork();
                    net.AddNodesAndLinks(diagram.Nodes.Where(n => CanLayoutPart(n, layout)),
                        diagram.Links.Where(l => CanLayoutPart(l, layout)));
                    layout.Network = net;
                }
                else
                {  // update all vertex.Bounds
                    foreach (var v in net.Vertexes)
                    {
                        var n = v.Node;
                        if (n != null) v.Bounds = n.Bounds;
                    }
                }
                // don't need to re-collect the appropriate Nodes and Links to pass to DoLayout
                IsLayingOut = true;
                layout.DoLayout(null, null);
                IsLayingOut = false;
                layout.ValidLayout = true;
                // DoLayout normally discards the Network; keep it for next time
                layout.Network = net;
            }
            else
            {
                base.PerformLayout();
            }
        }

        // node positioning by the layout shouldn't invalidate itself
        private bool IsLayingOut { get; set; }

        // invalidate the Diagram.Layout if node/link added/removed,
        // and remove any cached ForceDirectedNetwork
        public override void InvalidateLayout(Part p, LayoutChange change)
        {
            if (IsLayingOut) return;
            if (SkipsInvalidate) return;
            if (Diagram == null) return;
            if (p?.Layer == null || p.Layer.IsTemporary) return;
            if (Diagram.Layout is ContinuousForceDirectedLayout layout &&
                (change == LayoutChange.NodeAdded || change == LayoutChange.NodeRemoved ||
                 change == LayoutChange.LinkAdded || change == LayoutChange.LinkRemoved))
            {
                layout.ValidLayout = false;
                layout.Network = null;
            }
            else
            {
                base.InvalidateLayout(p, change);
            }
        }
    }
}
