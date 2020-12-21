
namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 11800, UnitTestConstants.NetworkPortRangeStart + 11999);
    }
}
