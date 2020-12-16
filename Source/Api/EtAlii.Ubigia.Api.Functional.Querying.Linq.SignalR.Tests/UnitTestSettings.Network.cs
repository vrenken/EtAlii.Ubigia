
namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 11400, UnitTestConstants.NetworkPortRangeStart + 11599);
    }
}
