namespace EtAlii.Ubigia.Api.Management
{
    using System.Threading.Tasks;

    public interface IAccountResolver
    {
        Task<Account> Get(IAccountInfoProvider accountInfoProvider, Account currentAccount, Storage currentStorage = null);
    }
}