namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    internal interface INextCompositeComponentIdAlgorithm
    {
        ulong Create(ContainerIdentifier containerIdentifier);
    }
}
