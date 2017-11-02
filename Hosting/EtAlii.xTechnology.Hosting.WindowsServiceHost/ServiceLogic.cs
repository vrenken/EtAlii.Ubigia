namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.User;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;

    public class ServiceLogic : IServiceLogic
    {
        private const string _nameFormat = "UIS${0}"; // Ubigia Infrastructure Service
        private const string _displayNameFormat = "Ubigia Infrastructure Service ({0})";
        private const string _descriptionFormat = "Provides applications access to the Ubigia storage '{0}'";
        

        // SERVINFSERV$SQLEXPRESS
        public string Name { get; }

        public string DisplayName { get; }

        public string Description { get; }

        private readonly IHost _host;

        public ServiceLogic()
        {
            var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configuration = new HostConfigurationBuilder().Build(sectionName => exeConfiguration.GetSection(sectionName), out IInfrastructure infrastructure);

            _host = new HostFactory<WindowsServiceHost>().Create(configuration);

            var infrastructureName = infrastructure.Configuration.Name;
            Name = String.Format(_nameFormat, infrastructureName).Replace(" ","_");
            DisplayName = String.Format(_displayNameFormat, infrastructureName);
            Description = String.Format(_descriptionFormat, infrastructureName);
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
