namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.Authentication
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IAdminAuthenticationServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
