namespace EtAlii.Servus.Client.Windows.Diagnostics.Views
{
    using System.Windows;
    using EtAlii.xTechnology.Workflow;

    public class ClickSelectingTool : Northwoods.GoXam.Tool.ClickSelectingTool
    {
        public ICommandProcessor CommandProcessor { get { return (ICommandProcessor)GetValue(CommandProcessorProperty); } set { SetValue(CommandProcessorProperty, value); } }
        public static readonly DependencyProperty CommandProcessorProperty = DependencyProperty.Register("CommandProcessor", typeof(ICommandProcessor), typeof(ClickSelectingTool), new PropertyMetadata());

        public ClickSelectingTool()
        {
        }

        public override void DoMouseUp()
        {
            base.DoMouseUp();

            if (IsDoubleClick() && this.Diagram.SelectedNode != null)
            {
                var node = this.Diagram.SelectedNode.Data as EntryNode;
      //          if(_entryInspectedConfiguration.AutoAdd)
                {
                    CommandProcessor.Process(new DiscoverEntryCommand(node.Entry, ProcessReason.Discovered, 1));
                }
            }
        } 
    }
}
