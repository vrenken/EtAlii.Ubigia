namespace EtAlii.Ubigia.Storage
{
    internal interface INextContainerIdentifierAlgorithm
    {
        ContainerIdentifier Create(ContainerIdentifier currentContainerIdentifier);
    }
}
