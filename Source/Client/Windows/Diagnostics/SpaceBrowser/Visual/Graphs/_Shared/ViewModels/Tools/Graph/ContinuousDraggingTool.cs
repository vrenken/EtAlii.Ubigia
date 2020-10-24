namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using Northwoods.GoXam;
    using Northwoods.GoXam.Tool;

    // each time we're dragging the selection, request a layout with a minimal number of iterations
    public class ContinuousDraggingTool : DraggingTool
    {
        public override void DoMouseMove()
        {
            base.DoMouseMove();
            if (!Active) return;
            if (!(Diagram.Layout is ContinuousForceDirectedLayout cfdLayout)) return;
            
            //LayoutManager mgr = Diagram.LayoutManager
            var oldIteration = cfdLayout.MaxIterations;
            // limit the number of iterations during dragging
            var numberOfNodes = Diagram.PartManager.NodesCount;
            cfdLayout.MaxIterations = Math.Max(1, (int)Math.Ceiling(10000.0 / (numberOfNodes * numberOfNodes)));
            // perform the layout right now
            Diagram.LayoutManager.LayoutDiagram(LayoutInitial.InvalidateAll, true);
            cfdLayout.MaxIterations = oldIteration;
        }
    }
}
