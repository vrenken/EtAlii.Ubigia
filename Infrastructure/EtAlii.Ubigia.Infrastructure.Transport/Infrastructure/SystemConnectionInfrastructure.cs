﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;

    public class SystemConnectionInfrastructure : InfrastructureBase
    {
        public SystemConnectionInfrastructure(
            IInfrastructureConfiguration configuration,
            ISpaceRepository spaces,
            IIdentifierRepository identifiers,
            IEntryRepository entries,
            IRootRepository roots,
            IAccountRepository accounts,
            IContentRepository content,
            IContentDefinitionRepository contentDefinition,
            IPropertiesRepository properties,
            IStorageRepository storages,
            ILogicalContext logicalContext)
            : base(configuration, spaces, identifiers, entries, roots, accounts, content, contentDefinition, properties, storages, logicalContext)
        {
        }

        public override Task Start()
        {
            // This action is needed because the Logical layer needs a fully functional system connection to do 
            // the initialization of the storage and spaces.
            // The functional is the only one that can provide these kind of connections.
            Configuration.SystemConnectionCreationProxy.Initialize(() =>
            {
                var configuration = new SystemConnectionConfiguration()
                    .Use(SystemTransportProvider.Create(this))
                    .Use(this);
                return new SystemConnectionFactory().Create(configuration);
            });

            return base.Start();
        }
    }
}