﻿namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Net.Http;
    using EtAlii.xTechnology.MicroContainer;

    public class SignalRSpaceTransport : SpaceTransportBase, ISignalRSpaceTransport
    {
		public string AuthenticationToken { get => _authenticationTokenGetter(); set => _authenticationTokenSetter(value); }
        private readonly Action<string> _authenticationTokenSetter;
        private readonly Func<string> _authenticationTokenGetter;
	    private readonly Func<HttpMessageHandler> _httpMessageHandlerFactory;
	    
        public SignalRSpaceTransport(
	        Uri address,
	        Func<HttpMessageHandler> httpMessageHandlerFactory,
			Action<string> authenticationTokenSetter, 
            Func<string> authenticationTokenGetter)
        : base(address)
        {
	        _httpMessageHandlerFactory = httpMessageHandlerFactory;
			_authenticationTokenSetter = authenticationTokenSetter;
            _authenticationTokenGetter = authenticationTokenGetter;
        }

	    public HttpMessageHandler HttpMessageHandlerFactory( )
	    {
		    return _httpMessageHandlerFactory();
		}

        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new SignalRSpaceClientsScaffolding()
            };
        }
    }
}
