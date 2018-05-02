namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Content
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserContentServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
