namespace EtAlii.Servus.Api.Tests
{
    public class TestInfrastructure : EtAlii.Servus.Infrastructure.Hosting.Tests.TestInfrastructureBase
    {
        public TestInfrastructure(string name)
            : base(name)
        {
        }

        public TestInfrastructure()
            : this("Unit test storage - Api")
        {
        }
    }
}
