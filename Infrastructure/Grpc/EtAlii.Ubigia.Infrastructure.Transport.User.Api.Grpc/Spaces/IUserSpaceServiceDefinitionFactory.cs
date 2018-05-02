namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Spaces
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserSpaceServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
