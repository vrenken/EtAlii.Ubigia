// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
	using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using Microsoft.AspNetCore.SignalR;

	public class InformationHub : HubBase
    {
	    private readonly IStorageRepository _storageRepository;
	    private readonly ServiceDetails _serviceDetails;
	    private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

		public InformationHub(
			ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
			IStorageRepository storageRepository,
			IInfrastructureOptions infrastructureOptions)
			: base(authenticationTokenVerifier)
		{
			_authenticationTokenVerifier = authenticationTokenVerifier;
			_storageRepository = storageRepository;
            _serviceDetails = infrastructureOptions.ServiceDetails.Single(sd => sd.Name == ServiceDetailsName.SignalR);
		}

		public async Task<Storage> GetLocalStorage()
		{
			var httpContext = Context.GetHttpContext();
			httpContext!.Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
			var authenticationToken = stringValues.Single();
			await _authenticationTokenVerifier.Verify(authenticationToken, Role.Admin, Role.System).ConfigureAwait(false);

			return await _storageRepository.GetLocal().ConfigureAwait(false);
		}

		public ConnectivityDetails GetLocalConnectivityDetails()
		{
			var result = new ConnectivityDetails
			{
                ManagementAddress = _serviceDetails.ManagementAddress.ToString(),
                DataAddress = _serviceDetails.DataAddress.ToString()
			};
			return result;
		}
    }
}
