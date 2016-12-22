namespace EtAlii.Servus.Storage
{
    using EtAlii.xTechnology.MicroContainer;

    public interface IStorageConfiguration
    {
        IStorageExtension[] Extensions { get; }
        string Name { get; }

        IStorageConfiguration Use(string name);
        IStorageConfiguration Use(IStorageExtension[] extensions);

        IStorage GetStorage(Container container);

        IStorageConfiguration Use<TStorage>()
            where TStorage : class, IStorage;

    }
}
