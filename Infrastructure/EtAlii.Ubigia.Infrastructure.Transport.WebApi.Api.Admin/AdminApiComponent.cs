namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Structure;
    using global::Owin;
    using Owin;

    public partial class AdminApiComponent : IAdminApiComponent
    {
        private readonly ISelector<Type, Func<Type, object>> _resolverSelector;

        public AdminApiComponent(Container container)
        {
            var storageRepository = container.GetInstance<IStorageRepository>();
            var accountRepository = container.GetInstance<IAccountRepository>();
            var spaceRepository = container.GetInstance<ISpaceRepository>();

            _resolverSelector = new Selector<Type, Func<Type, object>>()
                .Register(type => type == typeof(StorageController), type => new StorageController(storageRepository))
                .Register(type => type == typeof(AccountController), type => new AccountController(accountRepository))
                .Register(type => type == typeof(SpaceController), type => new SpaceController(spaceRepository));
        }

        public void Start(IAppBuilder application)
        {
            ////_logger.Info("Starting WebAPI services");

            //_httpConfiguration.MapHttpAttributeRoutes();
            //_httpConfiguration.Formatters.Add(new PayloadMediaTypeFormatter());

            ////if (_diagnostics.EnableLogging)
            ////{
            ////    _httpConfiguration.EnableSystemDiagnosticsTracing();
            ////}

            //application.UseWebApi(_httpConfiguration);

            //_httpConfiguration.EnsureInitialized();

            ////_logger.Info("Started WebAPI services");
        }

        public void Stop()
        {
        }

        public bool TryGetService(Type serviceType, out object serviceInstance)
        {
            var resolver = _resolverSelector.TrySelect(serviceType);
            if (resolver != null)
            {
                serviceInstance = resolver(serviceType);
                return true;
            }
            serviceInstance = null;
            return false;
        }
    }
}