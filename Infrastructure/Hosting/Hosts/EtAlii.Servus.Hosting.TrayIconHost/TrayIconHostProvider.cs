namespace EtAlii.Servus.Hosting.TrayIconHost
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Servus.Storage;
    using EtAlii.xTechnology.Logging;

    public class TrayIconHostProvider : ITrayIconHostProvider
    {
        public ITrayIconHost Host { get { return _host; } }
        private ITrayIconHost _host;

        public void Initialize(IHost host)
        {
            _host = (ITrayIconHost)host;
        }
    }
}
