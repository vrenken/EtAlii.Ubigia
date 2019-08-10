namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;

    public interface IGraphDocumentViewModel : IDocumentViewModel
    {
        /// <summary>
        /// The Configuration used to instantiate this ViewModel.
        /// </summary>
        IGraphConfiguration Configuration { get; }

        IGraphContext GraphContext { get; }

        EntryNode FindNodeByKey(Identifier identifier);
        
        void AddNode(EntryNode nodedata);
        void DoNodeAdded(EntryNode nodedata);
        
        void RemoveNode(EntryNode nodedata);
        void DoNodeRemoved(EntryNode nodedata);

        void RemoveLink(EntryLink linkdata);
        void DoLinkRemoved(EntryLink linkdata);

        IEnumerable<EntryLink> GetLinksForNode(EntryNode nodedata);

        bool IsLinked(EntryNode fromdata, string fromparam, EntryNode todata, string toparam);
        void AddLink(EntryLink linkdata);
        void DoLinkAdded(EntryLink linkdata);

        bool CommitTransaction(string tname);
        bool RollbackTransaction();
        bool StartTransaction(string tname);
    }
}
