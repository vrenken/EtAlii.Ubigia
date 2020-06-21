namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface ISpaceResolver
    {
        Task<Space> Get(ISpaceInfoProvider spaceInfoProvider, Space currentSpace, Account currentAccount);
    }
}