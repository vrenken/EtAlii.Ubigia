namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public interface IUserRootServiceDefinitionFactory
    {
        ServerServiceDefinition Create(IInfrastructure infrastructure, ISpaceAuthenticationInterceptor spaceAuthenticationInterceptor);
    }
}
