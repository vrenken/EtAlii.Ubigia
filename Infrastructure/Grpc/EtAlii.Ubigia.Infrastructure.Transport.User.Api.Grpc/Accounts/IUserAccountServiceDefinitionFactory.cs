namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Accounts
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserAccountServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
