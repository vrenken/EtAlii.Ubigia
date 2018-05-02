namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.Spaces
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IAdminSpaceServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure);
    }
}
