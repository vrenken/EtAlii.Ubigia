namespace EtAlii.Ubigia.Storage
{
    internal interface INextCompositeComponentIdAlgorithm
    {
        ulong Create(ContainerIdentifier containerIdentifier);
    }
}
