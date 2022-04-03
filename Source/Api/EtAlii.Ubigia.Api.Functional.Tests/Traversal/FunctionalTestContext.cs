// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using Microsoft.Extensions.Configuration;

    public class FunctionalTestContext : IFunctionalTestContext
    {
        public ILogicalTestContext Logical { get; }

        public IConfigurationRoot ClientConfiguration => Logical.ClientConfiguration;
        public IConfigurationRoot HostConfiguration => Logical.HostConfiguration;

        public FunctionalTestContext(ILogicalTestContext logical)
        {
            Logical = logical;
        }

        public async Task AddPeople(ITraversalContext context, ExecutionScope scope)
        {
            await AddJohnDoe(context, scope).ConfigureAwait(false);
            await AddJaneDoe(context, scope).ConfigureAwait(false);
            await AddTonyStark(context, scope).ConfigureAwait(false);
            await AddPeterBanner(context, scope).ConfigureAwait(false);
            await AddTanjaBanner(context, scope).ConfigureAwait(false);
            await AddArjanBanner(context, scope).ConfigureAwait(false);
            await AddIdaBanner(context, scope).ConfigureAwait(false);

            await AddFriends(context, scope).ConfigureAwait(false);
        }

        public async Task AddAddresses(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "Location:+=Europe/NL/Overijssel/Enschede/Lavenhorsthoek/23",
                "Location:+=Europe/NL/Overijssel/Enschede/Beltstraat/80",
                "Location:+=Europe/NL/Overijssel/Enschede/\"van Roenshof\"/17",
                "Location:+=Europe/DE/\"Nordrhein-Westfalen\"/Ahlen/\"Hensel-Lida-Strasse\"/12",
            };
            var addQuery = string.Join("\r\n", addQueries);


            await context.Process(addQuery, scope);
        }

        private async Task AddPeterBanner(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "person:+=Banner/Peter",
                "Person:Banner/Peter# <= FirstName",
                "person:Banner/Peter <= { Birthdate: 1977-06-27, NickName: \'Pete\', Lives: 1 }"
            };
            var addQuery = string.Join("\r\n", addQueries);


            await context.Process(addQuery, scope);
        }

        private async Task AddTanjaBanner(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "person:+=Banner/Tanja",
                "Person:Banner/Tanja# <= FirstName",
                "person:Banner/Tanja <= { Birthdate: 1970-02-03, NickName: \'LadyL\', Lives: 1 }"
            };
            var addQuery = string.Join("\r\n", addQueries);

            await context.Process(addQuery, scope);
        }

        private async Task AddArjanBanner(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "person:+=Banner/Arjan",
                "Person:Banner/Arjan# <= FirstName",
                "person:Banner/Arjan <= { Birthdate: 1988-06-23, NickName: \'Bengel\', Lives: 1 }"
            };
            var addQuery = string.Join("\r\n", addQueries);

            await context.Process(addQuery, scope);
        }


        private async Task AddIdaBanner(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "person:+=Banner/Ida",
                "Person:Banner/Ida# <= FirstName",
                "person:Banner/Ida <= { Birthdate: 1992-02-07, NickName: \'Scheetje\', Lives: 1 }"
            };
            var addQuery = string.Join("\r\n", addQueries);

            await context.Process(addQuery, scope);
        }

        private async Task AddJohnDoe(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "Person:+=Doe/John",
                "Person:Doe/John# <= FirstName",
                "person:Doe/John  <= { Birthdate: 1977-06-27, NickName: \'Johnny\', Lives: 1 }"
            };
            var addQuery = string.Join("\r\n", addQueries);


            await context.Process(addQuery, scope);
        }

        private async Task AddJaneDoe(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "person:+=Doe/Jane",
                "Person:Doe/Jane# <= FirstName",
                "person:Doe/Jane <= { Birthdate: 1970-02-03, NickName: \'Janey\', Lives: 2 }"
            };
            var addQuery = string.Join("\r\n", addQueries);


            await context.Process(addQuery, scope);
        }

        private async Task AddTonyStark(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "person:+=Stark/Tony",
                "Person:Stark/Tony# <= FirstName",
                "person:Stark/Tony <= { Birthdate: 1976-05-12, NickName: \'Iron Man\', Lives: 9 }",
            };
            var addQuery = string.Join("\r\n", addQueries);


            await context.Process(addQuery, scope);
        }

        private async Task AddFriends(ITraversalContext context, ExecutionScope scope)
        {
            var addQueries = new[]
            {
                "Person:Doe# <= FamilyName",
                "Person:Stark# <= FamilyName",
                "Person:Banner# <= FamilyName",

                "person:Banner/Tanja += Friends",
                "person:Banner/Peter += Friends",
                "person:Banner/Ida += Friends",
                "person:Doe/Jane += Friends",
                "person:Doe/John += Friends",
                "person:Stark/Tony += Friends",

                "person:Stark/Tony/Friends += person:Doe/John",
                "person:Stark/Tony/Friends += person:Doe/Jane",
                "person:Stark/Tony/Friends += person:Banner/Peter",

                "person:Doe/John/Friends += person:Stark/Tony",
                "person:Doe/John/Friends += person:Doe/Jane",

                "person:Doe/Jane/Friends += person:Doe/John",
                "person:Doe/Jane/Friends += person:Stark/Tony",

                "person:Banner/Arjan += Friends",
                "person:Banner/Arjan/Friends += person:Banner/Tanja",
                "person:Banner/Arjan/Friends += person:Banner/Peter",
                "person:Banner/Arjan/Friends += person:Banner/Ida",

                "person:Banner/Ida/Friends += person:Banner/Tanja",
                "person:Banner/Ida/Friends += person:Banner/Arjan",
                "person:Banner/Ida/Friends += person:Banner/Peter",

                "person:Banner/Peter/Friends += person:Banner/Tanja",
                "person:Banner/Peter/Friends += person:Banner/Arjan",
                "person:Banner/Peter/Friends += person:Banner/Ida",
                "person:Banner/Peter/Friends += person:Stark/Tony",

                "person:Banner/Tanja/Friends += person:Banner/Peter",
                "person:Banner/Tanja/Friends += person:Banner/Arjan",
                "person:Banner/Tanja/Friends += person:Banner/Ida",
            };
            var addQuery = string.Join("\r\n", addQueries);


            await context.Process(addQuery, scope);

//            var testQuery = context.Parse("person:Stark/Tony")
//            var testResult = await context.Process(testQuery.Script, new ExecutionScope())
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

        public async Task Start()
        {
            await Logical
                .Start()
                .ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await Logical
                .Stop()
                .ConfigureAwait(false);
        }
    }
}
