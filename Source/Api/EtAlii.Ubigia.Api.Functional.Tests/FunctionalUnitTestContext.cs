// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public partial class FunctionalUnitTestContext : IAsyncLifetime
    {
        public IFunctionalTestContext Functional { get; private set; }

        public ILogicalTestContext Logical => Functional.Logical;

        public FileComparer FileComparer { get; }
        public FolderComparer FolderComparer { get; }

        public IConfigurationRoot ClientConfiguration => Functional.Logical.Fabric.Transport.Host.ClientConfiguration;
        public IConfigurationRoot HostConfiguration => Functional.Logical.Fabric.Transport.Host.HostConfiguration;

        public FunctionalUnitTestContext()
        {
            FileComparer = new FileComparer();
            FolderComparer = new FolderComparer(FileComparer);
        }

        public async Task InitializeAsync()
        {
            Functional = new FunctionalTestContextFactory().Create();
            await Functional
                .Start(UnitTestSettings.NetworkPortRange)
                .ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await Functional.Stop().ConfigureAwait(false);
            Functional = null;
        }

        public async Task<(TFirst, TSecond)> CreateFunctional<TFirst, TSecond>()
        {
            var container = new Container();

            var options = await new FunctionalOptions(ClientConfiguration)
                .UseLapaParsing()
                .UseDataConnectionToNewSpace(this, true)
                .ConfigureAwait(false);

            foreach (var extension in options.GetExtensions<IFunctionalExtension>())
            {
                extension.Initialize(container);
            }

            return (container.GetInstance<TFirst>(), container.GetInstance<TSecond>());
        }

        public async Task<T> CreateFunctionalOnNewSpace<T>()
        {
            var options = await new FunctionalOptions(ClientConfiguration)
                .UseLapaParsing()
                .UseDataConnectionToNewSpace(this, true)
                .ConfigureAwait(false);

            return CreateFunctional<T>(options);
        }

        public async Task<(TFirst, TSecond)> CreateFunctionalOnNewSpace<TFirst, TSecond>()
        {
            var options = await new FunctionalOptions(ClientConfiguration)
                .UseLapaParsing()
                .UseDataConnectionToNewSpace(this, true)
                .ConfigureAwait(false);

            return CreateFunctional<TFirst, TSecond>(options);
        }

        public T CreateFunctional<T>(IFunctionalOptions options)
        {
            var container = new Container();

            foreach (var extension in options.GetExtensions<IFunctionalExtension>())
            {
                extension.Initialize(container);
            }

            return container.GetInstance<T>();
        }

        public (TFirst, TSecond) CreateFunctional<TFirst, TSecond>(IFunctionalOptions options)
        {
            var container = new Container();

            foreach (var extension in options.GetExtensions<IFunctionalExtension>())
            {
                extension.Initialize(container);
            }

            return (container.GetInstance<TFirst>(), container.GetInstance<TSecond>());
        }
    }
}
