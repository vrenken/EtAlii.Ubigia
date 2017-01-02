namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public interface IAccountInitializer
    {
        void Initialize(Account account, AccountTemplate template);
    }
}