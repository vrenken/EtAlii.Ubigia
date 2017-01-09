namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Fabric;

    public interface IGraphDocumentViewModel : IDocumentViewModel
    {
        IGraphConfiguration Configuration { get; }

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
