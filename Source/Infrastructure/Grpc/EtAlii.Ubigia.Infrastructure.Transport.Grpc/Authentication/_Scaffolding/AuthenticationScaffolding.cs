// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
{
	using EtAlii.xTechnology.MicroContainer;

	public class AuthenticationScaffolding : IScaffolding
	{
		public void Register(Container container)
		{
			container.Register<IAuthenticationTokenConverter, AuthenticationTokenConverter>();
			container.Register<ISimpleAuthenticationVerifier, SimpleAuthenticationVerifier>();
			container.Register<ISimpleAuthenticationBuilder, SimpleAuthenticationBuilder>();
			container.Register<ISimpleAuthenticationTokenVerifier, SimpleAuthenticationTokenVerifier>();
		}
	}
}