namespace EtAlii.Servus.Storage
{
    internal interface INextContainerIdentifierAlgorithm
    {
        ContainerIdentifier Create(ContainerIdentifier currentContainerIdentifier);
    }
}
