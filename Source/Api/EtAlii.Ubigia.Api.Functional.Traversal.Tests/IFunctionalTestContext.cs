namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;

    public interface IFunctionalTestContext
    {
        IDiagnosticsConfiguration Diagnostics { get; }

        Task ConfigureLogicalContextConfiguration(LogicalContextConfiguration configuration, bool openOnCreation);

        //Task<ILogicalContext> CreateLogicalContext(bool openOnCreation);

        Task AddPeople(ITraversalScriptContext context);
        Task AddAddresses(ITraversalScriptContext context);

        Task Start(PortRange portRange);
        Task Stop();
    }
}
