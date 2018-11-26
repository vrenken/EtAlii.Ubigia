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

        public async Task AddPeople(IDataContext context)
        {
            await AddJohnDoe(context);
            await AddJaneDoe(context);
            await AddTonyStark(context);
        }

        public async Task AddAddresses(IDataContext context)
        {
            var addQueries = new[]
            {
                "Location:+=Europe/NL/Overijssel/Enschede/Ravenhorsthoek/55",
                "Location:+=Europe/NL/Overijssel/Enschede/Beltstraat/80",
                "Location:+=Europe/NL/Overijssel/Enschede/\"van Loenshof\"/32",
                "Location:+=Europe/DE/\"Nordrhein-Westfalen\"/Ahlen/\"Luise-Hensel-Strasse\"/12",
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Scripts.Process(addQuery);
        }

        public async Task AddJohnDoe(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:Doe/John  <= { Birthdate: 1978-07-28, Nickname: \'Johnny\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Scripts.Process(addQuery);
        }
        
        public async Task AddJaneDoe(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Doe/Jane",
                "Person:Doe/Jane <= { Birthdate: 1980-03-04, Nickname: \'Janey\', Lives: 2 }"
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Scripts.Process(addQuery);
        }

        
        public async Task AddTonyStark(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Stark/Tony",
                "Person:Stark/Tony <= { Birthdate: 1976-05-12, Nickname: \'Iron Man\', Lives: 9 }"
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