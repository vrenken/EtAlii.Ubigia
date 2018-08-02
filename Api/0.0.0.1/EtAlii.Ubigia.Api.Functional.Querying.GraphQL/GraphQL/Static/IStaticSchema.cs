namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using GraphQL;
    using GraphQL.Types;

    internal interface IStaticSchema : ISchema
    {
        IDependencyResolver DependencyResolver { get; }
    }
}