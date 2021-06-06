
namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 11800, UnitTestConstants.NetworkPortRangeStart + 11999);
    }
}
