namespace EtAlii.Ubigia.Client.Windows.Diagnostics.Views
{
    using System.Windows;

    public class ClickSelectingTool : Northwoods.GoXam.Tool.ClickSelectingTool
    {
        public IGraphContext GraphContext { get { return (IGraphContext)GetValue(GraphContextProperty); } set { SetValue(GraphContextProperty, value); } }
        public static readonly DependencyProperty GraphContextProperty = DependencyProperty.Register("GraphContext", typeof(IGraphContext), typeof(ClickSelectingTool), new PropertyMetadata());

        public ClickSelectingTool()
        {
        }

        public override void DoMouseUp()
        {
            base.DoMouseUp();

            if (IsDoubleClick() && Diagram.SelectedNode != null)
            {
                var node = Diagram.SelectedNode.Data as EntryNode;
      //          if(_entryInspectedConfiguration.AutoAdd)
                {
                    GraphContext.CommandProcessor.Process(new DiscoverEntryCommand(node.Entry, ProcessReason.Discovered, 1), GraphContext.DiscoverEntryCommandHandler);
                }
            }
        } 
    }
}
