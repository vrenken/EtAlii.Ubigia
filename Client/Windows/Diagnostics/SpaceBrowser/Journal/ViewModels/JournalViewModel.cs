namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Mvvm;

    public class JournalViewModel : BindableBase, IJournal
    {
        public ObservableCollection<JournalItem> Items { get { return _items; } }
        private readonly ObservableCollection<JournalItem> _items;

        public int Size { get { return _size; } set { SetProperty(ref _size, value); } }
        private int _size = 300;

        protected IFabricContext Fabric { get { return _fabric; } }
        private readonly IFabricContext _fabric;
        private readonly MainDispatcherInvoker _mainDispatcherInvoker;

        private int _currentId = 0;

        public JournalViewModel(
            IFabricContext fabric,
            MainDispatcherInvoker mainDispatcherInvoker)
        {
            _items = new ObservableCollection<JournalItem>();
            _fabric = fabric;
            _mainDispatcherInvoker = mainDispatcherInvoker;
            _fabric.Entries.Prepared += OnEntryPrepared;
            _fabric.Entries.Stored += OnEntryStored;
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
            _mainDispatcherInvoker.SafeInvoke(delegate()
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
            };
        }
    }
}
