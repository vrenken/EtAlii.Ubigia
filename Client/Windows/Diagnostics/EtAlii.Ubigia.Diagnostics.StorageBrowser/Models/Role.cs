namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class Role : BindableBase
    {
        public string Name { get { return _name; } set { SetProperty(ref _name, value); } }
        private string _name;
    }
}
