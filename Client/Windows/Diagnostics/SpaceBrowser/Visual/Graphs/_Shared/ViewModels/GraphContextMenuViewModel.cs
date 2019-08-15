namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Linq;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Windows.Mvvm;

    public class GraphContextMenuViewModel : BindableBase, IGraphContextMenuViewModel
    {
        public ICommand HideChildrenCommand { get; }

        public ICommand ShowChildrenCommand { get; }

        public ICommand HideParentCommand { get; }

        public ICommand ShowParentCommand { get; }

        public ICommand HidePreviousCommand { get; }

        public ICommand ShowPreviousCommand { get; }

        public ICommand HideNextCommand { get; }

        public ICommand ShowNextCommand { get; }

        public ICommand HideDowndatesCommand { get; }

        public ICommand ShowDowndatesCommand { get; }

        public ICommand HideUpdatesCommand { get; }

        public ICommand ShowUpdatesCommand { get; }

        private readonly IGraphContext _graphContext;

        public GraphContextMenuViewModel(
            IGraphContext graphContext)  
        {
            _graphContext = graphContext;

            HideChildrenCommand = new RelayCommand(HideChildren, CanHideChildren);
            ShowChildrenCommand = new RelayCommand(ShowChildren, CanShowChildren);

            HideParentCommand = new RelayCommand(HideParent, CanHideParent);
            ShowParentCommand = new RelayCommand(ShowParent, CanShowParent);

            HidePreviousCommand = new RelayCommand(HidePrevious, CanHidePrevious);
            ShowPreviousCommand = new RelayCommand(ShowPrevious, CanShowPrevious);

            HideNextCommand = new RelayCommand(HideNext, CanHideNext);
            ShowNextCommand = new RelayCommand(ShowNext, CanShowNext);

            HideDowndatesCommand = new RelayCommand(HideDowndates, CanHideDowndates);
            ShowDowndatesCommand = new RelayCommand(ShowDowndates, CanShowDowndates);

            HideUpdatesCommand = new RelayCommand(HideUpdates, CanHideUpdates);
            ShowUpdatesCommand = new RelayCommand(ShowUpdates, CanShowUpdates);
        }

        private void ShowUpdates(object obj)
        {
            throw new System.NotImplementedException();
        }

        private bool CanShowUpdates(object obj)
        {
            return false;
        }

        private void HideUpdates(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process(new TraverseRelationsQuery(entry, e => e.Updates), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideUpdates(object obj)
        {
            return _graphContext.QueryProcessor.Process(new FindEntriesOnGraphQuery(((IReadOnlyEntry)obj).Updates), _graphContext.FindEntriesOnGraphQueryHandler).Any();
        }

        private void ShowDowndates(object obj)
        {
            // Show all downdates.
        }

        private bool CanShowDowndates(object obj)
        {
            return false;
        }

        private void HideDowndates(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process(new TraverseRelationQuery(entry, e => e.Downdate), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideDowndates(object obj)
        {
            return _graphContext.QueryProcessor.Process(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Downdate), _graphContext.FindEntryOnGraphQueryHandler).Any();
        }

        private void ShowNext(object obj)
        {
            // Show all next entries.
        }

        private bool CanShowNext(object obj)
        {
            return false;
        }

        private void HideNext(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process(new TraverseRelationQuery(entry, e => e.Next), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideNext(object obj)
        {
            return _graphContext.QueryProcessor.Process(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Next), _graphContext.FindEntryOnGraphQueryHandler).Any();
        }

        private void ShowPrevious(object obj)
        {
            // Show all previous entries.
        }

        private bool CanShowPrevious(object obj)
        {
            return false;
        }

        private void HidePrevious(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process(new TraverseRelationQuery(entry, e => e.Previous), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHidePrevious(object obj)
        {
            return _graphContext.QueryProcessor.Process(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Previous), _graphContext.FindEntryOnGraphQueryHandler).Any();
        }

        private void ShowParent(object obj)
        {
            // Show all parent entries.
        }

        private bool CanShowParent(object obj)
        {
            return false;
        }

        private void HideParent(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process(new TraverseRelationQuery(entry, e => e.Parent), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideParent(object obj)
        {
            return _graphContext.QueryProcessor.Process(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Parent), _graphContext.FindEntryOnGraphQueryHandler).Any();
        }

        private void ShowChildren(object obj)
        {
            // Show all child entries.
        }

        private bool CanShowChildren(object obj)
        {
            return false;
        }

        private void HideChildren(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process(new TraverseRelationsQuery(entry, e => e.Children), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideChildren(object obj)
        {
            return _graphContext.QueryProcessor.Process(new FindEntriesOnGraphQuery(((IReadOnlyEntry)obj).Children), _graphContext.FindEntriesOnGraphQueryHandler).Any();
        }
    }
}
