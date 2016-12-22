namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Client.Windows.Shared;
    using EtAlii.xTechnology.Mvvm;
    using EtAlii.xTechnology.Workflow;
    using System.Linq;
    using EtAlii.Servus.Windows;
    using ICommand = System.Windows.Input.ICommand;

    public class GraphContextMenuViewModel : BindableBase
    {
        public ICommand HideChildrenCommand { get { return _hideChildrenCommand; } }
        private readonly ICommand _hideChildrenCommand;

        public ICommand ShowChildrenCommand { get { return _showChildrenCommand; } }
        private readonly ICommand _showChildrenCommand;

        public ICommand HideParentCommand { get { return _hideParentCommand; } }
        private readonly ICommand _hideParentCommand;

        public ICommand ShowParentCommand { get { return _showParentCommand; } }
        private readonly ICommand _showParentCommand;

        public ICommand HidePreviousCommand { get { return _hidePreviousCommand; } }
        private readonly ICommand _hidePreviousCommand;

        public ICommand ShowPreviousCommand { get { return _showPreviousCommand; } }
        private readonly ICommand _showPreviousCommand;

        public ICommand HideNextCommand { get { return _hideNextCommand; } }
        private readonly ICommand _hideNextCommand;

        public ICommand ShowNextCommand { get { return _showNextCommand; } }
        private readonly ICommand _showNextCommand;

        public ICommand HideDowndatesCommand { get { return _hideDowndatesCommand; } }
        private readonly ICommand _hideDowndatesCommand;

        public ICommand ShowDowndatesCommand { get { return _showDowndatesCommand; } }
        private readonly ICommand _showDowndatesCommand;

        public ICommand HideUpdatesCommand { get { return _hideUpdatesCommand; } }
        private readonly ICommand _hideUpdatesCommand;

        public ICommand ShowUpdatesCommand { get { return _showUpdatesCommand; } }
        private readonly ICommand _showUpdatesCommand;

        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IUnitOfWorkProcessor _unitOfWorkProcessor;

        public GraphContextMenuViewModel(
            ICommandProcessor commandProcessor,
            IQueryProcessor queryProcessor,
            IUnitOfWorkProcessor unitOfWorkProcessor)  
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
            _unitOfWorkProcessor = unitOfWorkProcessor;

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
            var ids = _queryProcessor.Process<Identifier>(new TraverseRelationsQuery(entry, e => e.Updates));
            _commandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected));
        }

        private bool CanHideUpdates(object obj)
        {
            return _queryProcessor.Process<IReadOnlyEntry>(new FindEntriesOnGraphQuery(((IReadOnlyEntry)obj).Updates)).Any();
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
            var ids = _queryProcessor.Process<Identifier>(new TraverseRelationQuery(entry, e => e.Downdate));
            _commandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected));
        }

        private bool CanHideDowndates(object obj)
        {
            return _queryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Downdate)).Any();
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
            var ids = _queryProcessor.Process<Identifier>(new TraverseRelationQuery(entry, e => e.Next));
            _commandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected));
        }

        private bool CanHideNext(object obj)
        {
            return _queryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Next)).Any();
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
            var ids = _queryProcessor.Process<Identifier>(new TraverseRelationQuery(entry, e => e.Previous));
            _commandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected));
        }

        private bool CanHidePrevious(object obj)
        {
            return _queryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Previous)).Any();
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
            var ids = _queryProcessor.Process<Identifier>(new TraverseRelationQuery(entry, e => e.Parent));
            _commandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected));
        }

        private bool CanHideParent(object obj)
        {
            return _queryProcessor.Process<IReadOnlyEntry>(new FindEntryOnGraphQuery(((IReadOnlyEntry)obj).Parent)).Any();
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
            var ids = _queryProcessor.Process<Identifier>(new TraverseRelationsQuery(entry, e => e.Children));
            _commandProcessor.Process(new RemoveEntriesFromGraphCommand(ids, ProcessReason.Selected));
        }

        private bool CanHideChildren(object obj)
        {
            return _queryProcessor.Process<IReadOnlyEntry>(new FindEntriesOnGraphQuery(((IReadOnlyEntry)obj).Children)).Any();
        }
    }
}
