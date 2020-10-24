namespace EtAlii.Ubigia.Persistence
{
    internal interface INextCompositeComponentIdAlgorithm
    {
        ulong Create(ContainerIdentifier containerIdentifier);
    }
}
