namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.Properties
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserPropertiesServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
