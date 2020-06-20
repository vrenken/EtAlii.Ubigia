namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Windows.Mvvm;

    public class JournalViewModel : BindableBase, IJournalViewModel
    {
        public ObservableCollection<JournalItem> Items { get; }

        public int Size { get => _size; set => SetProperty(ref _size, value); }
        private int _size = 300;

        protected IFabricContext Fabric { get; }

        private readonly IMainDispatcherInvoker _mainDispatcherInvoker;

        private int _currentId;

        public JournalViewModel(
            IFabricContext fabric,
            IMainDispatcherInvoker mainDispatcherInvoker)
        {
            Items = new ObservableCollection<JournalItem>();
            Fabric = fabric;
            _mainDispatcherInvoker = mainDispatcherInvoker;
            Fabric.Entries.Prepared += OnEntryPrepared;
            Fabric.Entries.Stored += OnEntryStored;
        }

        private void OnEntryPrepared(Identifier identifier)
        {
            AddItem("Prepared", identifier.ToString());
        }

        private void OnEntryStored(Identifier identifier)
        {
            AddItem("Stored", identifier.ToString());
        }

        public void AddItem(string action, string description)
        {
            _mainDispatcherInvoker.SafeInvoke(delegate
            {
                _currentId += 1;
                Items.Add(new JournalItem { Id = _currentId, Action = action, Moment = DateTime.Now, Description = description });
                TruncateIfTooBig();
            });
        }

        private void TruncateIfTooBig()
        {
            while(Items.Count > Size)
            {
                if (Items.Count > 0)
                {
                    Items.RemoveAt(0);
                }
            }
        }
    }
}
