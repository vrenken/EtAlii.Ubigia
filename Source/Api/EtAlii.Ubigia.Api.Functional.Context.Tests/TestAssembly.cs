[assembly: JetBrains.dotMemoryUnit.SuppressXUnitOutputException]
[assembly: JetBrains.dotMemoryUnit.DotMemoryUnit(FailIfRunWithoutSupport = false)]

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    //you have to label the class with this or it is never scanned for methods
    internal class TestAssembly
    {
        public const string Category = "EtAlii.Ubigia.Api.Functional.Context";
    }
}
