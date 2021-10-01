// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR
{
	using System.Linq;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.xTechnology.Hosting;
	using Microsoft.AspNetCore.SignalR;

	public class InformationHub : HubBase
    {
	    private readonly IStorageRepository _storageRepository;
	    private readonly IConfigurationDetails _configurationDetails;
	    private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

		public InformationHub(
			ISimpleAuthenticationTokenVerifier authenticationTokenVerifier,
			IStorageRepository storageRepository,
			IConfigurationDetails configurationDetails)
			: base(authenticationTokenVerifier)
		{
			_authenticationTokenVerifier = authenticationTokenVerifier;
			_storageRepository = storageRepository;
			_configurationDetails = configurationDetails;
		}

		public Storage GetLocalStorage()
		{
			var httpContext = Context.GetHttpContext();
			httpContext.Request.Headers.TryGetValue("Authentication-Token", out var stringValues);
			var authenticationToken = stringValues.Single();
			_authenticationTokenVerifier.Verify(authenticationToken, Role.Admin, Role.System);

			return _storageRepository.GetLocal();
		}

		public ConnectivityDetails GetLocalConnectivityDetails()
		{
            if(!_configurationDetails.Paths.TryGetValue("AdminApiPathSignalR", out var adminApiPathSignalR))
            {
                adminApiPathSignalR = string.Empty;
            }

            if(!_configurationDetails.Paths.TryGetValue("UserApiPathSignalR", out var userApiPathSignalR))
            {
                userApiPathSignalR = string.Empty;
            }

			var result = new ConnectivityDetails
			{
				ManagementAddress = $"https://{_configurationDetails.Hosts["AdminHost"]}:{_configurationDetails.Ports["AdminApiPort"]}{_configurationDetails.Paths["AdminApiPath"]}{adminApiPathSignalR}",
				DataAddress = $"https://{_configurationDetails.Hosts["UserHost"]}:{_configurationDetails.Ports["UserApiPort"]}{_configurationDetails.Paths["UserApiPath"]}{userApiPathSignalR}",
			};
			return result;
		}
    }
}
