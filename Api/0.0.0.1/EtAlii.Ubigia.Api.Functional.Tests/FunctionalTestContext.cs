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
            await AddPeterVrenken(context);
            await AddTanjaVrenken(context);
            await AddArjanVrenken(context);
            await AddIdaVrenken(context);

            await AddFriends(context);
        }

        public async Task AddAddresses(IDataContext context)
        {
            var addQueries = new[]
            {
                "Location:+=Europe/NL/Overijssel/Enschede/Drienerlolaan/55",
                "Location:+=Europe/NL/Overijssel/Enschede/Beltstraat/80",
                "Location:+=Europe/NL/Overijssel/Enschede/\"van Loenshof\"/32",
                "Location:+=Europe/DE/\"Nordrhein-Westfalen\"/Ahlen/\"Luise-Hensel-Strasse\"/12",
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Scripts.Process(addQuery);
        }

        public async Task AddPeterVrenken(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Vrenken/Peter",
                "Person:Vrenken/Peter <= { Birthdate: 1978-07-28, Nickname: \'Pete\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Scripts.Process(addQuery);
        }

        public async Task AddTanjaVrenken(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Vrenken/Tanja",
                "Person:Vrenken/Tanja <= { Birthdate: 1980-03-04, Nickname: \'LadyL\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);

            await context.Scripts.Process(addQuery);
        }

        public async Task AddArjanVrenken(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Vrenken/Arjan",
                "Person:Vrenken/Arjan <= { Birthdate: 2015-08-13, Nickname: \'Bengel\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);

            await context.Scripts.Process(addQuery);
        }
        
        
        public async Task AddIdaVrenken(IDataContext context)
        {
            var addQueries = new[]
            {
                "Person:+=Vrenken/Ida",
                "Person:Vrenken/Ida <= { Birthdate: 2018-11-07, Nickname: \'Scheetje\', Lives: 1 }"
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
                "Person:Stark/Tony <= { Birthdate: 1976-05-12, Nickname: \'Iron Man\', Lives: 9 }",
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Scripts.Process(addQuery);
        }

        public async Task AddFriends(IDataContext context)
        {
            var addQueries = new[]
            {
                "$john <= Person:Doe/John",
                "$jane <= Person:Doe/Jane",
                "$tony <= Person:Stark/Tony",
                "$peter <= Person:Vrenken/Peter",
                "$tanja <= Person:Vrenken/Tanja",
                "$arjan <= Person:Vrenken/Arjan",
                "$ida <= Person:Vrenken/Ida",
                
                "Person:Stark/Tony/ += Friends",
                "Person:Stark/Tony/Friends += $john",
                "Person:Stark/Tony/Friends += $jane",
                "Person:Stark/Tony/Friends += $peter",
                
                "Person:Doe/John/ += Friends",
                "Person:Doe/John/Friends += $tony",
                "Person:Doe/John/Friends += $jane",
                
                "Person:Doe/Jane/ += Friends",
                "Person:Doe/Jane/Friends += $john",
                "Person:Doe/Jane/Friends += $tony",

                "Person:Vrenken/Peter/ += Friends",
                "Person:Vrenken/Peter/Friends += $tanja",
                "Person:Vrenken/Peter/Friends += $arjan",
                "Person:Vrenken/Peter/Friends += $ida",
                "Person:Vrenken/Peter/Friends += $tony",

                "Person:Vrenken/Tanja/ += Friends",
                "Person:Vrenken/Tanja/Friends += $peter",
                "Person:Vrenken/Tanja/Friends += $arjan",
                "Person:Vrenken/Tanja/Friends += $ida",

                "Person:Vrenken/Arjan/ += Friends",
                "Person:Vrenken/Arjan/Friends += $tanja",
                "Person:Vrenken/Arjan/Friends += $peter",
                "Person:Vrenken/Arjan/Friends += $ida",

                "Person:Vrenken/Ida/ += Friends",
                "Person:Vrenken/Ida/Friends += $tanja",
                "Person:Vrenken/Ida/Friends += $arjan",
                "Person:Vrenken/Ida/Friends += $peter",
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