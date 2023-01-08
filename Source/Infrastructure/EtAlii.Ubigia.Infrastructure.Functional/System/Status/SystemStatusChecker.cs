// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class SystemStatusChecker
    {
        public bool DetermineIfSetupIsNeeded()
        {
            return true;
        }

        public bool DetermineIfSystemIsOperational(
            IFunctionalContext functionalContext,
            IConfigurationRoot configurationRoot)
        {
            ArgumentNullException.ThrowIfNull(functionalContext);
            ArgumentNullException.ThrowIfNull(configurationRoot);

            var task = Task.Run(async () =>
            {
                using var systemConnection = functionalContext.SystemConnectionCreationProxy.Request();
                var (connection, _) = await systemConnection
                    .OpenSpace(AccountName.System, SpaceName.Configuration)
                    .ConfigureAwait(false);

                var options = new FabricOptions(configurationRoot)
                    .Use(connection)
                    .UseLogicalContext()
                    .UseFunctionalContext()
                    .UseAntlrParsing()
                    .UseDiagnostics();

                var context = Factory.Create<IGraphContext>(options);

                var scope = new ExecutionScope();

                var settings = await context
                    .ProcessGetServiceSettings(scope)
                    .ConfigureAwait(false);

                var isOperational = settings.IsOperational;
                isOperational &= !string.IsNullOrWhiteSpace(settings.AdminUsername);
                isOperational &= !string.IsNullOrWhiteSpace(settings.AdminPassword);
                isOperational &= !string.IsNullOrWhiteSpace(settings.Certificate);
                isOperational &= !string.IsNullOrWhiteSpace(settings.LocalStorageId);

                if (isOperational)
                {
                    isOperational &= Guid.TryParse(settings.LocalStorageId, out _);
                }
                return isOperational;
            });
            return task.GetAwaiter().GetResult();
        }
    }
}
