namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.ContentDefinition
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserContentDefinitionServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
