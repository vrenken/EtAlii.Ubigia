namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface IRootResolver
    {
        Task<Root> Get(IRootInfoProvider rootInfoProvider, Account currentAccount, Space currentSpace, Root currentRoot);
    }
}