namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public interface IAccountInitializer
    {
        Task Initialize(Account account, AccountTemplate template);
    }
}