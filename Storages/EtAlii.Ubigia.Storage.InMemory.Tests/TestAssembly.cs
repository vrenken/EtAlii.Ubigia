namespace EtAlii.Ubigia.Storage.InMemory.Tests
{
    using EtAlii.xTechnology.Logging;
    using Xunit;
    using System.Linq;
    using System.Reflection;

     //you have to label the class with this or it is never scanned for methods
    public class TestAssembly
    {
        public const string Category = "EtAlii.Ubigia.Storage.InMemory";
        public const string StorageName = "Unit test storage - InMemory Storage";
    }
}