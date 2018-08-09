namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class FunctionalTestContext : IFunctionalTestContext
    {
        private readonly ILogicalTestContext _logical;
        private readonly IDiagnosticsConfiguration _diagnostics;

        public FunctionalTestContext(
            ILogicalTestContext logical, 
            IDiagnosticsConfiguration diagnostics)
        {
            _logical = logical;
            _diagnostics = diagnostics;
        }

        public async Task<IDataContext> CreateFunctionalContext(bool openOnCreation)
        {
            var logicalContext = await _logical.CreateLogicalContext(openOnCreation);
            var configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(logicalContext);
            return new DataContextFactory().Create(configuration);
        }

        public async Task AddJohnDoe(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:Doe/John  <= { Birthdate: 1978-07-28, Nickname : \'Johnny\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Scripts.Process(addQuery);
        }

        
        public async Task AddTonyStark(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Stark/Tony",
                "Person:Stark/Tony <= { Birthdate: 1976-05-12, NickName : \'Iron Man\', Lives: 9 }"
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Scripts.Process(addQuery);
        }
        
        #region start/stop
        
        public async Task Start()
        {
            await _logical.Start();
        }

        public async Task Stop()
        {
            await _logical.Stop();
        }

        #endregion start/stop
    }
}