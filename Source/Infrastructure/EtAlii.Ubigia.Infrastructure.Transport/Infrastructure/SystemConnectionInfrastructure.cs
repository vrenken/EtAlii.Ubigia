// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Threading;

    public class SystemConnectionInfrastructure : InfrastructureBase
    {
        public SystemConnectionInfrastructure(
            IInfrastructureConfiguration configuration,
            IInformationRepository information,
            ISpaceRepository spaces,
            IIdentifierRepository identifiers,
            IEntryRepository entries,
            IRootRepository roots,
            IAccountRepository accounts,
            IContentRepository content,
            IContentDefinitionRepository contentDefinition,
            IPropertiesRepository properties,
            IStorageRepository storages,
            ILogicalContext logicalContext,
            IContextCorrelator contextCorrelator)
            : base(configuration, information, spaces, identifiers, entries, roots, accounts, content, contentDefinition, properties, storages, logicalContext, contextCorrelator)
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
                    .Use(new SystemTransportProvider(this))
                    .Use(this);
                return new SystemConnectionFactory().Create(configuration);
            });

            return base.Start();
        }
    }
}
