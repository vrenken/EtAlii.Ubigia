namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Entries
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserEntryServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
