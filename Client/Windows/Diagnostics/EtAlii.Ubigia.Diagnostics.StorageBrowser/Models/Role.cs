namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.xTechnology.Mvvm;

    public class Role : BindableBase
    {
        public string Name { get { return _name; } set { SetProperty(ref _name, value); } }
        private string _name;
    }
}
