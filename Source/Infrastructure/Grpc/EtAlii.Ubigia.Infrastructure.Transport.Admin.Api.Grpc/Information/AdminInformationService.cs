// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System.Linq;
    using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.Management.Grpc;
	using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using global::Grpc.Core;

	public class AdminInformationService : InformationGrpcService.InformationGrpcServiceBase, IAdminInformationService
    {
	    private readonly IStorageRepository _items;
        private readonly ServiceDetails _serviceDetails;

	    public AdminInformationService(
            IStorageRepository items,
            IInfrastructureOptions infrastructureOptions)
        {
            _items = items;
            _serviceDetails = infrastructureOptions.ServiceDetails.Single(sd => sd.Name == ServiceDetailsName.Grpc);
        }

		public override async Task<LocalStorageResponse> GetLocalStorage(LocalStorageRequest request, ServerCallContext context)
		{
			var storage = await _items.GetLocal().ConfigureAwait(false);

			var response = new LocalStorageResponse
			{
				Storage = storage.ToWire()
			};
			return response;
		}

		public override Task<ConnectivityDetailsResponse> GetLocalConnectivityDetails(ConnectivityDetailsRequest request, ServerCallContext context)
		{
			var response = new ConnectivityDetailsResponse
			{
				ConnectivityDetails = new ConnectivityDetails
				{
                    ManagementAddress = _serviceDetails.ManagementAddress.ToString(),
                    DataAddress = _serviceDetails.DataAddress.ToString()
				}
			};
			return Task.FromResult(response);
		}
    }
}
