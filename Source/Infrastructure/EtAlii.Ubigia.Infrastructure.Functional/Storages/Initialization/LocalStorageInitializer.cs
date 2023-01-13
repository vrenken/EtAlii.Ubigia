// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Fabric;
using EtAlii.Ubigia.Api.Functional;
using EtAlii.Ubigia.Api.Functional.Antlr;
using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.Ubigia.Api.Logical;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.Extensions.Configuration;

internal class LocalStorageInitializer : ILocalStorageInitializer
{
    private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;

    private readonly IConfigurationSection _configuration;
    private readonly IConfigurationRoot _configurationRoot;

    public LocalStorageInitializer(IConfigurationRoot configurationRoot, ISystemConnectionCreationProxy systemConnectionCreationProxy)
    {
        _systemConnectionCreationProxy = systemConnectionCreationProxy;

        _configuration = configurationRoot.GetSection("Infrastructure:Functional:Setup");
        _configurationRoot = configurationRoot;
    }

    public async Task Initialize(Storage localStorage)
    {
        // Create a system connection.
        var systemConnection = _systemConnectionCreationProxy.Request();

        // Setup both the system and admin space.
        await InitializeSpaces(systemConnection).ConfigureAwait(false);

        // And configure the ServiceSettings in the system space.
        await InitializeServiceSettings(systemConnection, localStorage).ConfigureAwait(false);
    }

    private async Task InitializeSpaces(ISystemConnection systemConnection)
    {
        var systemAccountName = _configuration.GetValue<string>("DefaultSystemAccountName");
        var systemAccountPassword = _configuration.GetValue<string>("DefaultSystemAccountPassword");

        var administratorAccountName = _configuration.GetValue<string>("DefaultAdministratorAccountName");
        var administratorAccountPassword = _configuration.GetValue<string>("DefaultAdministratorAccountPassword");

        // Create a management connection.
        var managementConnection = await systemConnection
            .OpenManagementConnection()
            .ConfigureAwait(false);

        // Add the system user.
        await managementConnection.Accounts
            .Add(systemAccountName, systemAccountPassword, AccountTemplate.System)
            .ConfigureAwait(false);

        // Add the admin user.
        await managementConnection.Accounts
            .Add(administratorAccountName, administratorAccountPassword, AccountTemplate.Administrator)
            .ConfigureAwait(false);

        await managementConnection
            .Close()
            .ConfigureAwait(false);

    }

    private async Task InitializeServiceSettings(ISystemConnection systemConnection, Storage localStorage)
    {
        var (connection, _) = await systemConnection
            .OpenSpace(AccountName.System, SpaceName.Configuration)
            .ConfigureAwait(false);

        var options = new FabricOptions(_configurationRoot)
            .Use(connection)
            .UseLogicalContext()
            .UseFunctionalContext()
            .UseAntlrParsing()
            .UseDiagnostics();

        var context = Factory.Create<IGraphContext>(options);

        var scope = new ExecutionScope();

        var administratorAccountName = _configuration.GetValue<string>("DefaultAdministratorAccountName");
        var administratorAccountPassword = _configuration.GetValue<string>("DefaultAdministratorAccountPassword");

        scope.Variables["adminUsername"] = new ScopeVariable(administratorAccountName, "Variable");
        scope.Variables["adminPassword"] = new ScopeVariable(administratorAccountPassword, "Variable");
        scope.Variables["isOperational"] = new ScopeVariable(true, "Variable");
        scope.Variables["showSetup"] = new ScopeVariable(true, "Variable");

        // TODO: The certificate cannot be set initially.
        scope.Variables["certificate"] = new ScopeVariable("BaadFood", "Variable");
        // TODO: The local storage ID probably needs to be orchestrated differently.
        scope.Variables["localStorageId"] = new ScopeVariable(localStorage.Id.ToString(), "Variable");

        await context
            .ProcessSetServiceSettings(scope)
            .ConfigureAwait(false);
    }
}
