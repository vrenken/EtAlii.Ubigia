namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using Northwoods.GoXam;
    using Northwoods.GoXam.Layout;

    // layout should not change position of selected nodes
    public class ContinuousForceDirectedLayout : ForceDirectedLayout
    {
        public ContinuousForceDirectedLayout()
        {
            Conditions = LayoutChange.None;
        }

        // selected nodes are "fixed"
        protected override bool IsFixed(ForceDirectedVertex v)
        {
            Node n = v.Node;
            if (n != null) return n.IsSelected;
            return base.IsFixed(v);
        }

        // don't position "fixed" nodes
        protected override void LayoutNodes()
        {
            foreach (ForceDirectedVertex vertex in Network.Vertexes)
            {
                if (!IsFixed(vertex)) vertex.CommitPosition();
            }
        }
    }
}
