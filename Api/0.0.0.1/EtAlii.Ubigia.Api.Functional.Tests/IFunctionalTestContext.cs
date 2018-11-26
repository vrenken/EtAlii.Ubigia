namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;

    public interface IFunctionalTestContext
    {
        Task<IDataContext> CreateFunctionalContext(bool openOnCreation);

        Task AddPeople(IDataContext context);
        Task AddAddresses(IDataContext context);

        Task Start();
        Task Stop();
    }
}