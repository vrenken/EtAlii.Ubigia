namespace EtAlii.Ubigia.Infrastructure.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Structure;

    public class MicroContainerDependencyResolver : IDependencyResolver
    {
        private readonly ISelector<Type, Func<Type, object>> _resolverSelector;

        public MicroContainerDependencyResolver(Container container)
        {
            var storageRepository = container.GetInstance<IStorageRepository>();
            var accountRepository = container.GetInstance<IAccountRepository>();
            var spaceRepository = container.GetInstance<ISpaceRepository>();
            var entryRepository = container.GetInstance<IEntryRepository>();
            var contentRepository = container.GetInstance<IContentRepository>();
            var contentDefinitionRepository = container.GetInstance<IContentDefinitionRepository>();
            var propertiesRepository = container.GetInstance<IPropertiesRepository>();
            var rootRepository = container.GetInstance<IRootRepository>();

            _resolverSelector = new Selector<Type, Func<Type, object>>()
                .Register(type => type == typeof(AuthenticateController), type => new AuthenticateController())
                .Register(type => type == typeof(StorageController), type => new StorageController(storageRepository))
                .Register(type => type == typeof(AccountController), type => new AccountController(accountRepository))
                .Register(type => type == typeof(SpaceController), type => new SpaceController(spaceRepository))
                .Register(type => type == typeof(EntryController), type => new EntryController(entryRepository))
                .Register(type => type == typeof(ContentController), type => new ContentController(contentRepository))
                .Register(type => type == typeof(ContentDefinitionController), type => new ContentDefinitionController(contentDefinitionRepository))
                .Register(type => type == typeof(PropertiesController), type => new PropertiesController(propertiesRepository))
                .Register(type => type == typeof(RootController), type => new RootController(rootRepository))
                // Additional registrations.
                .Register(type => type == typeof(IdentifierBinder), type => new IdentifierBinder())
                .Register(type => type == typeof(IdentifiersBinder), type => new IdentifiersBinder())
                // And finally usage of the instances from the container.
                .Register(type => type.Assembly == typeof(MicroContainerDependencyResolver).Assembly, type => container.GetInstance(type))
                .Register(type => true, type => null);
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            var resolver = _resolverSelector.Select(serviceType);
            return resolver(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            serviceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var resolver = _resolverSelector.Select(serviceType);
            var result = (IEnumerable<object>)resolver(serviceType);
            return result ?? Enumerable.Empty<object>();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}