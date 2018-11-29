
namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Types;

    internal class IdDirectiveResult : DirectiveResult
    {
        public IdMapping[] Mappings { get; set; }
    }
}