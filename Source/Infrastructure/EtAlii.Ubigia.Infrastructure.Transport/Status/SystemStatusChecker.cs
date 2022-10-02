// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class SystemStatusChecker
    {
        public bool DetermineIfSystemIsOperational(List<IService> services, IConfigurationRoot configurationRoot)
        {
            var task = Task.Run<bool>(async () =>
            {
                var infrastructureService = services
                    .OfType<IInfrastructureService>()
                    .Single();

                using var systemConnection = infrastructureService.Infrastructure.Options.SystemConnectionCreationProxy.Request();
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
                    .ProcessServiceSettings2()
                    .ConfigureAwait(false);

                // var isOperational = true;
                // isOperational &= !string.IsNullOrWhiteSpace(settings.AdminUsername);
                // isOperational &= !string.IsNullOrWhiteSpace(settings.AdminPassword);
                // isOperational &= !string.IsNullOrWhiteSpace(settings.Certificate);
                return true;//isOperational;
            });
            return task.GetAwaiter().GetResult();
        }
    }
}
