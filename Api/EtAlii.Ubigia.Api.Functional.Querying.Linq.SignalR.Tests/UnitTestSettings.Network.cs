#pragma warning disable CS0436    
namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using EtAlii.xTechnology.Hosting;

    public static class UnitTestSettings
    {
        public static PortRange NetworkPortRange = new PortRange(UnitTestConstants.NetworkPortRangeStart + 11400, UnitTestConstants.NetworkPortRangeStart + 11599);
    }
}