namespace EtAlii.Ubigia.Client.Windows.Diagnostics.Views
{
    using Northwoods.GoXam;
    using Northwoods.GoXam.Tool;
    using System;

    // each time we're dragging the selection, request a layout with a minimal number of iterations
    public class ContinuousDraggingTool : DraggingTool
    {
        public override void DoMouseMove()
        {
            base.DoMouseMove();
            if (Active)
            {
                var cfdlayout = Diagram.Layout as ContinuousForceDirectedLayout;
                if (cfdlayout != null)
                {
                    LayoutManager mgr = Diagram.LayoutManager;
                    int olditer = cfdlayout.MaxIterations;
                    // limit the number of iterations during dragging
                    int numnodes = Diagram.PartManager.NodesCount;
                    cfdlayout.MaxIterations = Math.Max(1, (int)Math.Ceiling(10000.0 / (numnodes * numnodes)));
                    // perform the layout right now
                    Diagram.LayoutManager.LayoutDiagram(LayoutInitial.InvalidateAll, true);
                    cfdlayout.MaxIterations = olditer;
                }
            }
        }
    }
}
