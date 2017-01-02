namespace EtAlii.Servus.Storage.Tests
{
    using EtAlii.Servus.Storage;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Configuration : IStorageConfiguration
    {
        public string Name { get; set; }
    }
}
