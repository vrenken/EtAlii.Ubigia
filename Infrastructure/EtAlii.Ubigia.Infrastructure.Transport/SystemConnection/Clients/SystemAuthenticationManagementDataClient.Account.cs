namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    internal partial class SystemAuthenticationManagementDataClient
    {
        private Task<Account> GetAccount(string accountName)
        {
            var account = _infrastructure.Accounts.Get(accountName);
            if (account == null)
            {
                string message = $"Unable to connect using the specified account ({accountName})";
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return Task.FromResult(account);
        }
    }
}
