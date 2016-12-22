namespace EtAlii.Servus.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Management;
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
