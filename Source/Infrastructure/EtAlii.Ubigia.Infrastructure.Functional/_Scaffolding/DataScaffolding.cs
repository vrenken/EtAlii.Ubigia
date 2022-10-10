// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class DataScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IInformationRepository, InformationRepository>();

            container.Register<IRootRepository, RootRepository>();
            container.Register<IEntryRepository, EntryRepository>();
            container.Register<IContentRepository, ContentRepository>();
            container.Register<IContentDefinitionRepository, ContentDefinitionRepository>();
            container.Register<IPropertiesRepository, PropertiesRepository>();
            container.Register<IIdentifierRepository, IdentifierRepository>();

            container.Register<IAccountInitializer, AccountInitializer>();
            container.Register<ISpaceInitializer, DirectSpaceInitializer>();
        }
    }
}
