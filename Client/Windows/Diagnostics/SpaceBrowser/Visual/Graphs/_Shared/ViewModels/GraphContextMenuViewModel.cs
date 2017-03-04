namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Mvvm;
    using System.Linq;
    using EtAlii.Ubigia.Windows;
    using ICommand = System.Windows.Input.ICommand;

    public class GraphContextMenuViewModel : BindableBase, IGraphContextMenuViewModel
    {
        public ICommand HideChildrenCommand => _hideChildrenCommand;
        private readonly ICommand _hideChildrenCommand;

        public ICommand ShowChildrenCommand => _showChildrenCommand;
        private readonly ICommand _showChildrenCommand;

        public ICommand HideParentCommand => _hideParentCommand;
        private readonly ICommand _hideParentCommand;

        public ICommand ShowParentCommand => _showParentCommand;
        private readonly ICommand _showParentCommand;

        public ICommand HidePreviousCommand => _hidePreviousCommand;
        private readonly ICommand _hidePreviousCommand;

        public ICommand ShowPreviousCommand => _showPreviousCommand;
        private readonly ICommand _showPreviousCommand;

        public ICommand HideNextCommand => _hideNextCommand;
        private readonly ICommand _hideNextCommand;

        public ICommand ShowNextCommand => _showNextCommand;
        private readonly ICommand _showNextCommand;

        public ICommand HideDowndatesCommand => _hideDowndatesCommand;
        private readonly ICommand _hideDowndatesCommand;

        public ICommand ShowDowndatesCommand => _showDowndatesCommand;
        private readonly ICommand _showDowndatesCommand;

        public ICommand HideUpdatesCommand => _hideUpdatesCommand;
        private readonly ICommand _hideUpdatesCommand;

        public ICommand ShowUpdatesCommand => _showUpdatesCommand;
        private readonly ICommand _showUpdatesCommand;

        private readonly IGraphContext _graphContext;

        public GraphContextMenuViewModel(
            IGraphContext graphContext)  
        {
            _graphContext = graphContext;

            _hideChildrenCommand = new RelayCommand(HideChildren, CanHideChildren);
            _showChildrenCommand = new RelayCommand(ShowChildren, CanShowChildren);

            _hideParentCommand = new RelayCommand(HideParent, CanHideParent);
            _showParentCommand = new RelayCommand(ShowParent, CanShowParent);

            _hidePreviousCommand = new RelayCommand(HidePrevious, CanHidePrevious);
            _showPreviousCommand = new RelayCommand(ShowPrevious, CanShowPrevious);

            _hideNextCommand = new RelayCommand(HideNext, CanHideNext);
            _showNextCommand = new RelayCommand(ShowNext, CanShowNext);

            _hideDowndatesCommand = new RelayCommand(HideDowndates, CanHideDowndates);
            _showDowndatesCommand = new RelayCommand(ShowDowndates, CanShowDowndates);

            _hideUpdatesCommand = new RelayCommand(HideUpdates, CanHideUpdates);
            _showUpdatesCommand = new RelayCommand(ShowUpdates, CanShowUpdates);
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
            var ids = _graphContext.QueryProcessor.Process<Identifier>(new TraverseRelationsQuery(entry, e => e.Updates), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideUpdates(object obj)
        {
            return _graphContext.QueryProcessor.Process<IReadOnlyEntry>(new FindEntriesOnGraphQuery(((IReadOnlyEntry)obj).Updates), _graphContext.FindEntriesOnGraphQueryHandler).Any();
        }

        private void ShowDowndates(object obj)
        {
        }

        private bool CanShowDowndates(object obj)
        {
            return false;
        }

        private void HideDowndates(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process<Identifier>(new TraverseRelationQuery(entry, e => e.Downdate), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideDowndates(object obj)
        {
            return _graphContext.QueryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Downdate), _graphContext.FindEntryOnGraphQueryHandler).Any();
        }

        private void ShowNext(object obj)
        {
        }

        private bool CanShowNext(object obj)
        {
            return false;
        }

        private void HideNext(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process<Identifier>(new TraverseRelationQuery(entry, e => e.Next), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideNext(object obj)
        {
            return _graphContext.QueryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Next), _graphContext.FindEntryOnGraphQueryHandler).Any();
        }

        private void ShowPrevious(object obj)
        {
        }

        private bool CanShowPrevious(object obj)
        {
            return false;
        }

        private void HidePrevious(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process<Identifier>(new TraverseRelationQuery(entry, e => e.Previous), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHidePrevious(object obj)
        {
            return _graphContext.QueryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Previous), _graphContext.FindEntryOnGraphQueryHandler).Any();
        }

        private void ShowParent(object obj)
        {
        }

        private bool CanShowParent(object obj)
        {
            return false;
        }

        private void HideParent(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process<Identifier>(new TraverseRelationQuery(entry, e => e.Parent), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideParent(object obj)
        {
            return _graphContext.QueryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Parent), _graphContext.FindEntryOnGraphQueryHandler).Any();
        }

        private void ShowChildren(object obj)
        {
        }

        private bool CanShowChildren(object obj)
        {
            return false;
        }

        private void HideChildren(object obj)
        {
            var entry = (IReadOnlyEntry)obj;
            var ids = _graphContext.QueryProcessor.Process<Identifier>(new TraverseRelationsQuery(entry, e => e.Children), _graphContext.TraverseRelationsQueryHandler);
            _graphContext.CommandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected), _graphContext.RemoveEntriesFromGraphCommandHandler);
        }

        private bool CanHideChildren(object obj)
        {
            return _graphContext.QueryProcessor.Process<IReadOnlyEntry>(new FindEntriesOnGraphQuery(((IReadOnlyEntry)obj).Children), _graphContext.FindEntriesOnGraphQueryHandler).Any();
        }
    }
}
