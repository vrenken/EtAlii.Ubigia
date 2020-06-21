namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface IAccountResolver
    {
        Task<Account> Get(IAccountInfoProvider accountInfoProvider, Account currentAccount, Storage currentStorage = null);
    }
}