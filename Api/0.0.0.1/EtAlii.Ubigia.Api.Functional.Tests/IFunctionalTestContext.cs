namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;

    public interface IFunctionalTestContext
    {
        Task<IDataContext> CreateFunctionalContext(bool openOnCreation);

        Task AddPeople(IGraphSLScriptContext context);
        Task AddAddresses(IGraphSLScriptContext context);

        Task Start();
        Task Stop();
    }
}