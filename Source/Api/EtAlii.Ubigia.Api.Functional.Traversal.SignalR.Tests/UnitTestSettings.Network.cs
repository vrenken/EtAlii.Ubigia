
namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 12000, UnitTestConstants.NetworkPortRangeStart + 12199);
    }
}
