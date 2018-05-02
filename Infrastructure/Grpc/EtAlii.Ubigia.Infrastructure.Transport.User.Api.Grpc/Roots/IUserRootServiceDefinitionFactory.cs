namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Roots
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserRootServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
