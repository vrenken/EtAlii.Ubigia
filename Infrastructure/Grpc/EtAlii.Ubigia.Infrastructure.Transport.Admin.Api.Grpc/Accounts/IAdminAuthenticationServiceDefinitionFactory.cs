namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IAdminAccountServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
