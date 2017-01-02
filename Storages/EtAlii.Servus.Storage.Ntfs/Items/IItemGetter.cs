namespace EtAlii.Servus.Storage
{
    using System;

    public interface IItemGetter
    {
        Guid[] Get(ContainerIdentifier container);
    }
}
