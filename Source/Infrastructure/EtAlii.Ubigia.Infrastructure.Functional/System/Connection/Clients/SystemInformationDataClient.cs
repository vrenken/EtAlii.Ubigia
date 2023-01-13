// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;

internal sealed class SystemInformationDataClient : SystemStorageClientBase, IInformationDataClient
{
    private readonly IFunctionalContext _functionalContext;

    public SystemInformationDataClient(IFunctionalContext functionalContext)
    {
        _functionalContext = functionalContext;
    }

    public Task<Storage> GetConnectedStorage(ISpaceConnection connection, string address)
    {
        if (connection.Storage != null)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
        }

        return _functionalContext.Storages.GetLocal();
    }

    /// <inheritdoc />
    public Task<Storage> GetConnectedStorage(IStorageConnection connection)
    {
        if (connection.Storage != null)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
        }

        return _functionalContext.Storages.GetLocal();
    }

    /// <inheritdoc />
    public Task<ConnectivityDetails> GetConnectivityDetails(IStorageConnection connection)
    {
        var serviceDetails = _functionalContext.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the connectivity details with.

        var result = new ConnectivityDetails
        {
            ManagementAddress = serviceDetails.ManagementAddress.ToString(),
            DataAddress = serviceDetails.DataAddress.ToString(),
        };
        return Task.FromResult(result);
    }
}
