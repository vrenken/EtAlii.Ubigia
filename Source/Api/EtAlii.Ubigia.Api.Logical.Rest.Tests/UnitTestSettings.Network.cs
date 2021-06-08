
namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static readonly PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 12800, UnitTestConstants.NetworkPortRangeStart + 12999);
    }
}
