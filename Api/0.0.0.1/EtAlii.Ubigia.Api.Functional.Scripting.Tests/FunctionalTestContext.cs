namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical;
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

//        public async Task<IDataContext> CreateFunctionalContext(bool openOnCreation)
//        [
//            var logicalContext = await _logical.CreateLogicalContext(openOnCreation)
//            var configuration = new DataContextConfiguration()
//                .Use(_diagnostics)
//                .Use(logicalContext)
//            return new DataContextFactory().Create(configuration)
//        }

        
        public async Task<ILogicalContext> CreateLogicalContext(bool openOnCreation)
        {
            return await _logical.CreateLogicalContext(openOnCreation);
        }

        public IGraphSLScriptContext CreateGraphSLScriptContext(ILogicalContext logicalContext)
        {
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(logicalContext)
                .Use(_diagnostics);
            return new GraphSLScriptContextFactory().Create(configuration);
        }
        
        public IGraphQLQueryContext CreateGraphQLQueryContext(ILogicalContext logicalContext)
        {
            var configuration = new GraphQLQueryContextConfiguration()
                .Use(logicalContext)
                .Use(_diagnostics);
            return new GraphQLQueryContextFactory().Create(configuration);
        }
        
        
        public async Task AddPeople(IGraphSLScriptContext context)
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

        public async Task AddAddresses(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "Location:+=Europe/NL/Overijssel/Enschede/Drienerlolaan/55",
                "Location:+=Europe/NL/Overijssel/Enschede/Beltstraat/80",
                "Location:+=Europe/NL/Overijssel/Enschede/\"van Loenshof\"/32",
                "Location:+=Europe/DE/\"Nordrhein-Westfalen\"/Ahlen/\"Luise-Hensel-Strasse\"/12",
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Process(addQuery);
        }

        public async Task AddPeterVrenken(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "person:+=Vrenken/Peter",
                "person:Vrenken/Peter <= { Birthdate: 1978-07-28, Nickname: \'Pete\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Process(addQuery);
        }

        public async Task AddTanjaVrenken(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "person:+=Vrenken/Tanja",
                "person:Vrenken/Tanja <= { Birthdate: 1980-03-04, Nickname: \'LadyL\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);

            await context.Process(addQuery);
        }

        public async Task AddArjanVrenken(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "person:+=Vrenken/Arjan",
                "person:Vrenken/Arjan <= { Birthdate: 2015-08-13, Nickname: \'Bengel\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);

            await context.Process(addQuery);
        }
        
        
        public async Task AddIdaVrenken(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "person:+=Vrenken/Ida",
                "person:Vrenken/Ida <= { Birthdate: 2018-11-07, Nickname: \'Scheetje\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);

            await context.Process(addQuery);
        }
        
        public async Task AddJohnDoe(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "person:+=Doe/John",
                "person:Doe/John  <= { Birthdate: 1978-07-28, Nickname: \'Johnny\', Lives: 1 }"
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Process(addQuery);
        }
        
        public async Task AddJaneDoe(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "person:+=Doe/Jane",
                "person:Doe/Jane <= { Birthdate: 1980-03-04, Nickname: \'Janey\', Lives: 2 }"
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Process(addQuery);
        }

        public async Task AddTonyStark(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "person:+=Stark/Tony",
                "person:Stark/Tony <= { Birthdate: 1976-05-12, Nickname: \'Iron Man\', Lives: 9 }",
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Process(addQuery);
        }

        public async Task AddFriends(IGraphSLScriptContext context)
        {
            var addQueries = new[]
            {
                "person:Doe <= { Type: 'FamilyName' }",
                "person:Stark <= { Type: 'FamilyName' }",
                "person:Vrenken <= { Type: 'FamilyName' }",

                "person:Vrenken/Tanja += Friends",
                "person:Vrenken/Peter += Friends",
                "person:Vrenken/Ida += Friends",
                "person:Doe/Jane += Friends",
                "person:Doe/John += Friends",
                "person:Stark/Tony += Friends",
                
                "person:Stark/Tony/Friends += person:Doe/John",
                "person:Stark/Tony/Friends += person:Doe/Jane",
                "person:Stark/Tony/Friends += person:Vrenken/Peter",
                
                "person:Doe/John/Friends += person:Stark/Tony",
                "person:Doe/John/Friends += person:Doe/Jane",
                
                "person:Doe/Jane/Friends += person:Doe/John",
                "person:Doe/Jane/Friends += person:Stark/Tony",

                "person:Vrenken/Arjan += Friends",
                "person:Vrenken/Arjan/Friends += person:Vrenken/Tanja",
                "person:Vrenken/Arjan/Friends += person:Vrenken/Peter",
                "person:Vrenken/Arjan/Friends += person:Vrenken/Ida",

                "person:Vrenken/Ida/Friends += person:Vrenken/Tanja",
                "person:Vrenken/Ida/Friends += person:Vrenken/Arjan",
                "person:Vrenken/Ida/Friends += person:Vrenken/Peter",

                "person:Vrenken/Peter/Friends += person:Vrenken/Tanja",
                "person:Vrenken/Peter/Friends += person:Vrenken/Arjan",
                "person:Vrenken/Peter/Friends += person:Vrenken/Ida",
                "person:Vrenken/Peter/Friends += person:Stark/Tony",
                
                "person:Vrenken/Tanja/Friends += person:Vrenken/Peter",
                "person:Vrenken/Tanja/Friends += person:Vrenken/Arjan",
                "person:Vrenken/Tanja/Friends += person:Vrenken/Ida",

//                "person:Vrenken/Ida/Friends/",
//                "person:Vrenken/Arjan/Friends/",
//                "person:Vrenken/Tanja/Friends/",
//                "person:Vrenken/Peter/Friends/",
//                "person:Stark/Tony/Friends/",
//                "person:Doe/Jane/Friends/",
//                "person:Doe/John/Friends/",
//                "person:Stark/Tony",
            };
            var addQuery = String.Join("\r\n", addQueries);


            await context.Process(addQuery);

//            var testQuery = context.Parse("person:Stark/Tony")
//            var testResult = await context.Process(testQuery.Script, new ScriptScope())
//            var result = await testResult.Output.ToArray()
//            var result = context.Scripts.Process(addQuery)
//
//            var list = await result.ToArray()
//
//            var ida = await list[list.Length - 6].Output.ToArray()
//            var arjan = await list[list.Length - 5].Output.ToArray()
//            var tanja = await list[list.Length - 4].Output.ToArray()
//            var peter = await list[list.Length - 3].Output.ToArray()
//            var tony = await list[list.Length - 3].Output.ToArray()
//            var jane = await list[list.Length - 2].Output.ToArray()
//            var john = await list[list.Length - 1].Output.ToArray()
//            var tonyNode = await list[list.Length - 1].Output.ToArray()
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