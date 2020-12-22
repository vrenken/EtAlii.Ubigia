
namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new(UnitTestConstants.NetworkPortRangeStart + 10600, UnitTestConstants.NetworkPortRangeStart + 10799);
    }
}
