namespace EtAlii.Ubigia.Persistence
{
    internal interface INextContainerIdentifierAlgorithm
    {
        ContainerIdentifier Create(ContainerIdentifier currentContainerIdentifier);
    }
}
