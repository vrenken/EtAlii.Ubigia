// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System.Net.Http;

	public interface ISignalRTransport
	{
		HttpMessageHandler HttpMessageHandlerFactory();

		//Uri Address [ get ]
		string AuthenticationToken { get; set; }
    }
}
