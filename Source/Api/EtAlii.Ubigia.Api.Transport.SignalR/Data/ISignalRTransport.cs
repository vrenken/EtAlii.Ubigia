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