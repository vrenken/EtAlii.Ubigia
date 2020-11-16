namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using GraphQL.Types;

    public interface IGraphTypeServiceProvider : IServiceProvider
    {
        void Register(GraphType graphType);
    }
}