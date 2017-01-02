//namespace EtAlii.Ubigia.Provisioning
//{
//    using EtAlii.Ubigia.Api;
//    using SimpleInjector;
//    using System;
//    using System.Collections.Generic;

//    public abstract class App
//    {
//        public readonly Container Container;
//        private readonly Lazy<IProviderConfiguration> _configuration;

//        protected App()
//        {
//            Container = new Container();
//            Container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

//            _configuration = new Lazy<IProviderConfiguration>(GetConfiguration);
//        }

//        public static void Setup<T>()
//            where T: App, new()
//        {
//            var app = new T();

//            app.RegisterTypes();
//            app.Container.Verify();

//            _current = app;
//        }

//        protected virtual void RegisterTypes()
//        {
//            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Providers");
//            EtAlii.Ubigia.Provisioning.SubSystem.Setup(Container, _configuration.Value, GetComponents, diagnostics);
//        }
//    }
//}
