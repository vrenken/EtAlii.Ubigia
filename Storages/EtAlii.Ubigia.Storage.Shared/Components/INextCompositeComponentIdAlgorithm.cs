namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    internal interface INextCompositeComponentIdAlgorithm
    {
        ulong Create(ContainerIdentifier containerIdentifier);
    }
}
