namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class ServiceLogic : IServiceLogic
    {
        //private const string _nameFormat = "UIS${0}"; // Ubigia Infrastructure Service
        //private const string _displayNameFormat = "Ubigia Infrastructure Service ({0})";
        //private const string _descriptionFormat = "Provides applications access to the Ubigia storage '{0}'";


        // SERVINFSERV$SQLEXPRESS
        public string Name { get; }

        public string DisplayName { get; }

        public string Description { get; }

        private readonly IHost _host;

        public ServiceLogic(IHostConfiguration configuration)
        {
            _host = new HostFactory<WindowsServiceHost>().Create(configuration);
        }


        public void Start(IEnumerable<string> args)
        {
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
