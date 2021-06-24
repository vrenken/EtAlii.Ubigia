// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.Management.Grpc;
	using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.xTechnology.Hosting;
	using global::Grpc.Core;

	public class AdminInformationService : InformationGrpcService.InformationGrpcServiceBase, IAdminInformationService
    {
	    private readonly IStorageRepository _items;
	    private readonly IConfigurationDetails _configurationDetails;

	    public AdminInformationService(IStorageRepository items, IConfigurationDetails configurationDetails)
	    {
		    _items = items;
		    _configurationDetails = configurationDetails;
	    }


		public override Task<LocalStorageResponse> GetLocalStorage(LocalStorageRequest request, ServerCallContext context)
		{
			var storage = _items.GetLocal();

			var response = new LocalStorageResponse
			{
				Storage = storage.ToWire()
			};
			return Task.FromResult(response);
		}

		public override Task<ConnectivityDetailsResponse> GetLocalConnectivityDetails(ConnectivityDetailsRequest request, ServerCallContext context)
		{
			var response = new ConnectivityDetailsResponse
			{
				ConnectivityDetails = new ConnectivityDetails
				{
					ManagementAddress = $"http://{_configurationDetails.Hosts["AdminHost"]}:{_configurationDetails.Ports["AdminPort"]}",
					DataAddress = $"http://{_configurationDetails.Hosts["UserHost"]}:{_configurationDetails.Ports["UserPort"]}",
				}
			};
			return Task.FromResult(response);
		}
    }
}
