namespace EtAlii.Servus.Infrastructure.Model
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.Servus.Storage;
    using System;

    public abstract class SingleFolderRepositoryBase<T> : RepositoryBase 
        where T : class, IIdentifiable
    {
        public SingleFolderRepositoryBase(IItemStorage itemStorage, IHostingConfiguration configuration)
            : base(itemStorage, configuration)
        {
        }
    }
}