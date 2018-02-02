namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Structure;
    using global::Owin;

    public class UserApiComponent : IUserApiComponent
    {
        private readonly ISelector<Type, Func<Type, object>> _resolverSelector;

        public UserApiComponent(Container container)
        {
            var entryRepository = container.GetInstance<IEntryRepository>();
            var contentRepository = container.GetInstance<IContentRepository>();
            var contentDefinitionRepository = container.GetInstance<IContentDefinitionRepository>();
            var propertiesRepository = container.GetInstance<IPropertiesRepository>();
            var rootRepository = container.GetInstance<IRootRepository>();

            _resolverSelector = new Selector<Type, Func<Type, object>>()
                .Register(type => type == typeof(EntryController), type => new EntryController(entryRepository))
                .Register(type => type == typeof(ContentController), type => new ContentController(contentRepository))
                .Register(type => type == typeof(ContentDefinitionController), type => new ContentDefinitionController(contentDefinitionRepository))
                .Register(type => type == typeof(PropertiesController), type => new PropertiesController(propertiesRepository))
                .Register(type => type == typeof(RootController), type => new RootController(rootRepository));
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