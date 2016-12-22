namespace EtAlii.Servus.Infrastructure.Shared
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Storage;
    using EtAlii.xTechnology.Logging;
    using Newtonsoft.Json;
    using SimpleInjector;
    using SimpleInjector.Extensions;
    using System;

    public abstract class App
    {
        public const string CompanyName = "EtAlii";
        public const string ProductName = "Servus";

        public static App Current { get { return _current; } }
        private static App _current;

        public readonly Container Container;
        private readonly Lazy<IHostingConfiguration> _configuration;

        protected App()
        {
            Container = new Container();
            _configuration = new Lazy<IHostingConfiguration>(GetConfiguration);
        }

        public static void Setup<T>(bool replaceExistingApp = true)
            where T: App, new()
        {
            if (_current == null || replaceExistingApp)
            {
                var app = new T();

                app.RegisterTypes();
                app.Container.Verify();

                _current = app;
            }
            //else if(_current.GetType() != typeof(T))
            //{
            //    throw new InvalidOperationException("Application setup is done twice with a different configuration");
            //}
        }

        protected virtual void RegisterTypes()
        {
            var enableProfiling = false;
            var enableLogging = false;
            var enableDebugging = true;

            EtAlii.Servus.Storage.SubSystem.Setup(Container, _configuration.Value, enableProfiling, enableLogging, enableDebugging);
            EtAlii.Servus.Storage.Ntfs.SubSystem.Setup(Container);

            EtAlii.Servus.Infrastructure.Shared.SubSystem.Setup(Container, _configuration.Value, enableProfiling, enableLogging, enableDebugging);
            EtAlii.Servus.Infrastructure.SubSystem.Setup(Container, enableProfiling, enableLogging, enableDebugging);
        }

        protected abstract IHostingConfiguration GetConfiguration();
    }
}