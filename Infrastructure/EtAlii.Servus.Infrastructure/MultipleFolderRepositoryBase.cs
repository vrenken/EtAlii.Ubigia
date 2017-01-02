namespace EtAlii.Servus.Infrastructure.Model
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.Servus.Storage;

    public abstract class MultipleFolderRepositoryBase<T> : RepositoryBase
        where T : class, IIdentifiable
    {
        public MultipleFolderRepositoryBase(IItemStorage itemStorage, IHostingConfiguration configuration)
            : base(itemStorage, configuration)
        {
        }
    }
}