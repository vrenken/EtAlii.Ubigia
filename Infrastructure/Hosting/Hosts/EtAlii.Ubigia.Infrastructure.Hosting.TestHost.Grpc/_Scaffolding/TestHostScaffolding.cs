﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
	//using EtAlii.Ubigia.Api.Transport.WebApi
	using EtAlii.xTechnology.MicroContainer;

	public class TestHostScaffolding : IScaffolding
	{
		public void Register(Container container)
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
		//}
	}
}
