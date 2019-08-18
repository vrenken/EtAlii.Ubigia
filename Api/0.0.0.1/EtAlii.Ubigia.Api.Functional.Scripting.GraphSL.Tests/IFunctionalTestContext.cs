namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Diagnostics;

    public interface IFunctionalTestContext
    {
        IDiagnosticsConfiguration Diagnostics { get; }
        
        Task ConfigureLogicalContextConfiguration(LogicalContextConfiguration configuration, bool openOnCreation);
        
        //Task<ILogicalContext> CreateLogicalContext(bool openOnCreation);

        Task AddPeople(IGraphSLScriptContext context);
        Task AddAddresses(IGraphSLScriptContext context);

        Task Start();
        Task Stop();
    }
}