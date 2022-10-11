// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Rest
{
    using EtAlii.Ubigia.Api.Transport.Rest;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;

    internal class RestStorageClientsScaffolding : IScaffolding
    {
        private readonly IRestInfrastructureClient _infrastructureClient;

        public RestStorageClientsScaffolding(IRestInfrastructureClient infrastructureClient)
        {
            _infrastructureClient = infrastructureClient;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IAddressFactory, AddressFactory>();
            container.Register<IStorageConnection, RestStorageConnection>();

            container.Register<IAuthenticationManagementDataClient, RestAuthenticationManagementDataClient>();

            container.Register<IInformationDataClient, RestInformationDataClient>();
            container.Register<IStorageDataClient, RestStorageDataClient>();
            container.Register<IAccountDataClient, RestAccountDataClient>();
            container.Register<ISpaceDataClient, RestSpaceDataClient>();

            if (_infrastructureClient != null)
            {
                container.Register(() => _infrastructureClient);
            }
            else
            {
                container.Register<IRestInfrastructureClient, RestInfrastructureClient>();
                container.Register(() => Serializer.Default);
                container.Register<IHttpClientFactory, RestHttpClientFactory>();
            }

        }
    }
}
