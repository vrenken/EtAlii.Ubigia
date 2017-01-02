namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.MicroContainer;
    using System;
    using System.IO;
    using HashLib;
    using Newtonsoft.Json;

    public class ContentManagerFactory
    {
        internal ContentManagerFactory()
        {
        }

        public IContentManager Create(IDataConnection connection)
        {
            var container = new Container();

            container.Register<IDataConnection>(() => connection, Lifestyle.Singleton);
            container.Register<IContentManager, ContentManager>(Lifestyle.Singleton);
            container.Register<IHash>(HashFactory.Checksum.CreateCRC64_ECMA, Lifestyle.Singleton);
            container.Register<IContentPartStoreCommandHandler, ContentPartStoreCommandHandler>(Lifestyle.Singleton);
            container.Register<IContentPartQueryHandler, ContentPartQueryHandler>(Lifestyle.Singleton);
            container.Register<IContentDefinitionQueryHandler, ContentDefinitionQueryHandler>(Lifestyle.Singleton);
            container.Register<IContentQueryHandler, ContentQueryHandler>(Lifestyle.Singleton);
            container.Register<IContentNewQueryHandler, ContentNewQueryHandler>(Lifestyle.Singleton);
            container.Register<IContentPartCalculator, ContentPartCalculator>(Lifestyle.Singleton);

            return container.GetInstance<IContentManager>();
        }
    }
}
