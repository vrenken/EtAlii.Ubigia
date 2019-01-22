namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using Northwoods.GoXam.Layout;

    // layout should not change position of selected nodes
    public class TreeLayout : Northwoods.GoXam.Layout.TreeLayout
    {
        public TreeLayout()
        {
            BreadthLimit = 1000;
            Alignment = TreeAlignment.Start;
        }
    }
}
