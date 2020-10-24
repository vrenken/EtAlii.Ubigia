namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    public interface IAccountInitializer
    {
        Task Initialize(Account account, AccountTemplate template);
    }
}