// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
	//using EtAlii.Ubigia.Api.Transport.Rest
	using EtAlii.xTechnology.MicroContainer;

	public class TestHostScaffolding : IScaffolding
	{
		public void Register(IRegisterOnlyContainer container)
		{
			//container.Register<IHost, TestHost>()
			//container.Register<IAddressFactory, AddressFactory>()
			//container.Register(() => CreateTestInfrastructureClient(container))
		}

		//private IInfrastructureClient CreateTestInfrastructureClient(Container container)
		//[
		//	var infrastructure = (TestInfrastructure)container.GetInstance<IInfrastructure>()
		//	var httpClientFactory = new TestHttpClientFactory(infrastructure)
		//	return new DefaultInfrastructureClient(httpClientFactory)
		//]
	}
}
