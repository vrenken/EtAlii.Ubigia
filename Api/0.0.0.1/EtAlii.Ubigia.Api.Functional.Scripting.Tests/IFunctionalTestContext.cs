namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public interface IFunctionalTestContext
    {
        Task<ILogicalContext> CreateLogicalContext(bool openOnCreation);
        IGraphSLScriptContext CreateGraphSLScriptContext(ILogicalContext logicalContext);
        IGraphQLQueryContext CreateGraphQLQueryContext(ILogicalContext logicalContext);

        Task AddPeople(IGraphSLScriptContext context);
        Task AddAddresses(IGraphSLScriptContext context);

        Task Start();
        Task Stop();
    }
}