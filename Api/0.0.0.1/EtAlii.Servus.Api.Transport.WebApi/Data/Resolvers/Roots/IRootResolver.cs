namespace EtAlii.Servus.Api.Management
{
    using System.Threading.Tasks;

    public interface IRootResolver
    {
        Task<Root> Get(IRootInfoProvider rootInfoProvider, Account currentAccount, Space currentSpace, Root currentRoot);
    }
}