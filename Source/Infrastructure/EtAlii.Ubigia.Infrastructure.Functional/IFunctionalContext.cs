// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.xTechnology.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    public interface IFunctionalContext
    {
        IContextCorrelator ContextCorrelator { get; }
        /// <summary>
        /// The Options used to instantiate this Infrastructure.
        /// </summary>
        FunctionalContextOptions Options { get; }

        /// <summary>
        /// All meta-information related operations can be found here.
        /// </summary>
        IInformationRepository Information { get; }

        /// <summary>
        /// All storage related operations can be found here.
        /// </summary>
        IStorageRepository Storages { get; }

        /// <summary>
        /// All space related operations can be found here.
        /// </summary>
        ISpaceRepository Spaces { get; }

        /// <summary>
        /// All entry related operations can be found here.
        /// </summary>
        IEntryRepository Entries { get; }

        /// <summary>
        /// All property related operations can be found here.
        /// </summary>
        IPropertiesRepository Properties { get; }

        /// <summary>
        /// All root related operations can be found here.
        /// </summary>
        IRootRepository Roots { get; }

        /// <summary>
        /// All account related operations can be found here.
        /// </summary>
        IAccountRepository Accounts { get; }

        /// <summary>
        /// All content related operations can be found here.
        /// </summary>
        IContentRepository Content { get; }

        /// <summary>
        /// All content definition related operations can be found here.
        /// </summary>
        IContentDefinitionRepository ContentDefinition { get ; }

        /// <summary>
        /// A proxy wrapping system connection creation mechanisms.
        /// </summary>
        ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }

        ILogicalContext LogicalContext { get; }

        Task Start();
        Task Stop();
    }
}
