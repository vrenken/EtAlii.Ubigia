namespace EtAlii.Ubigia.Infrastructure.Transport
{
    public interface ISimpleAuthenticationBuilder
	{
		string Build(string accountName, string hostIdentifier);
    }
}