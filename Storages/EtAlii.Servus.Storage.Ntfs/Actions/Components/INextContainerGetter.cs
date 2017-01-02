namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System.Collections.Generic;

    public interface INextContainerGetter
    {
        ContainerIdentifier GetNextContainer(ContainerIdentifier container);
    }
}
