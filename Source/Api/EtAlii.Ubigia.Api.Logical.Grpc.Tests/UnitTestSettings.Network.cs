
namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 12400, UnitTestConstants.NetworkPortRangeStart + 12599);
    }
}
