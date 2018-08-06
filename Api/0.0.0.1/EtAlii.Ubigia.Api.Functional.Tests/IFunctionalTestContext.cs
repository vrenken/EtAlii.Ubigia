namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;

    public interface IFunctionalTestContext
    {
        Task<IDataContext> CreateFunctionalContext(bool openOnCreation);

        Task AddJohnDoe(IDataContext context);
        Task AddTonyStark(IDataContext context);
            
        Task Start();
        Task Stop();
    }
}