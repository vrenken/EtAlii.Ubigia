namespace EtAlii.Ubigia.Api.Transport.SignalR
{
	using System.Net.Http;

	public interface ISignalRTransport
	{
		HttpMessageHandler HttpMessageHandlerFactory();

		string AuthenticationToken { get; set; }
    }
}