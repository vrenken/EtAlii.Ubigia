namespace EtAlii.Servus.Infrastructure.Functional
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    public interface IAccountInitializer
    {
        void Initialize(Account account, AccountTemplate template);
    }
}