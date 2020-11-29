namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Windows;

    public class ClickSelectingTool : Northwoods.GoXam.Tool.ClickSelectingTool
    {
        // ReSharper disable InconsistentNaming
        public IGraphContext GraphContext { get => (IGraphContext)GetValue(GraphContextProperty); set => SetValue(GraphContextProperty, value); }
        public static readonly DependencyProperty GraphContextProperty = DependencyProperty.Register("GraphContext", typeof(IGraphContext), typeof(ClickSelectingTool), new PropertyMetadata());
        // ReSharper restore InconsistentNaming

        public override void DoMouseUp()
        {
            base.DoMouseUp();

            if (IsDoubleClick() && Diagram.SelectedNode != null)
            {
                var node = (EntryNode)Diagram.SelectedNode.Data;
      //          if[_entryInspectedConfiguration.AutoAdd]
      //          [
                    GraphContext.CommandProcessor.Process(new DiscoverEntryCommand(node.Entry, ProcessReason.Discovered, 1), GraphContext.DiscoverEntryCommandHandler);
      //          ]
            }
        } 
    }
}
