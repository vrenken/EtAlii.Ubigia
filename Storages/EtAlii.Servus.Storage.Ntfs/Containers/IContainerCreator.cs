namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public interface IContainerCreator
    {
        void Create(ContainerIdentifier containerToCreate);
    }
}
