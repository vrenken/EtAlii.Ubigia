namespace EtAlii.Servus.Infrastructure.Model
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.Servus.Storage;

    public abstract class RepositoryBase<T> : RepositoryBase
        where T : class, IIdentifiable
    {
        public RepositoryBase(IStorage storage, IHostingConfiguration configuration)
            : base(storage, configuration)
        {
        }
    }
}